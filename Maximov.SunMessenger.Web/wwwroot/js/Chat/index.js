function Model() {

    const onMessageReceiveArray = [function (chatId, message) { }];
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();
    const users = [];
    const chats = [];
    const messages = {};

    var model = {
        get onMessageReceive() {
            return function (chatId, message) {
                onMessageReceiveArray.forEach(function (el) {
                    el(chatId, message);
                });
            }
        },
        set onMessageReceive(value) {
            onMessageReceiveArray.push(value);
        },

        getDirectChatId(userId) {
            let chatId = chats.find(c => c.userIds.some(id=>id==userId))?.id;
            if (chatId == null) {
                $.getJSON({
                    url: "/Chat/GetDirectChatIdByUser",
                    data: { userId: userId },
                    async: false,
                    success: function (id) {
                        chatId = id;
                    }
                });
            }
            return chatId;
        },

        //получает все чаты от самого свежего к самому старому
        getChats() {
            return chats;
        },

        //получения чата
        getChat(chatId) {
            let chat = chats.find(c => c.id == chatId);
            if (chat == null) {
                $.getJSON({
                    url: "/Chat/GetChatDetails",
                    data: { chatId: chatId },
                    async: false,
                    success: function (gettedChat) {
                        chat = gettedChat;
                        chats.push(chat);
                    }
                })
            }
            return chat;
        },

        getLoadedMessages(chatId) {
            return messages[chatId];
        },

        // поолучение сообщений отсортированы от самого нового к самому старому 
        //chatId: id чата
        //beforeDate: получает сообщения до указанной даты. Если null, то сообщения загружаются с самого нового
        //count: количество сообщений. Если null, то загружаются все сообщения
        getMessages(chatId, count) {
            if (messages[chatId] == null) {
                $.getJSON({
                    url: "/Chat/GetMessages",
                    data: {
                        chatId: chatId,
                        beforeDate: null,
                        count: null
                    },
                    async: false,
                    success: function (messageList) {
                        messages[chatId] = messageList;
                    }
                })
            }
            if (count == null)
                count = Number.MAX_VALUE;
            return messages[chatId].slice(0, count);
        },

        getUser(userId) {
            if (users[userId] == null) {
                $.getJSON({
                    url: "/User/Find",
                    data: { userId: userId },
                    success: function (user) {
                        users[userId] = user;
                    },
                    async: false
                });
            }
            return users[userId];
        },

        sendMessage(chatId, text) {
            hubConnection.invoke("Send", chatId, text);
        }
    }
    //загрузка всех чатов
    $.getJSON({
        url: "/Chat/GetChats",
        async: false,
        success: function (chatsWithLastMessage) {
            $.each(chatsWithLastMessage, function (i, chatWithMessage) {
                chats.push(chatWithMessage.chat);
            })
        }
    })

    hubConnection.on("Receive", function (chatId, message) {
        if (messages[chatId] == null)
            messages[chatId] == [];
        messages[chatId].unshift(message);
        model.onMessageReceive(chatId, message);
    });

    hubConnection.start();

    return model;
}

function SidebarViewManager(model) {
    var sidebarViewManager = {
        onCreateDirectChatClick() { },
        onChatClick(chatId) { },
        showChats() {
            var chats = model.getChats();
            prepandChats(chats);
        },
        updateChatOnNewMessage(chatId, message) {
            var chatElement = $(".sidebar>.chats>.chat").has(".chat__id:contains('" + chatId + "')");
            if (chatElement[0] == null) {
                var chat = model.getChat(chatId);
                let rows = "<a class='chat'>";
                rows += "<span class='chat__id'>" + chat.id + "</span>";
                rows += "<div class='chat__icon'></div>";
                rows += "<div class='chat__container'>";
                rows += "<span class='chat__name'>" + chat.name + "</span></br>";
                rows += "<span class='chat__message'>" + message.text + "</span></br>";
                //rows += "<span class='chat__date'>" + chat.message.date + "</span>";
                rows += "</div>";
                rows += "</a>";
                $(".sidebar>.chats").prepend(rows);
            }
            else {
                chatElement.prependTo(".sidebar>.chats").find(".chat__message").text(message.text);
            }
        }
    }

    //chats: чаты отсортированные от самого нового к самому старому
    function prepandChats(chats) {
        var rows = "";
        chats.forEach(function (chat) {
            let message = model.getMessages(chat.id, 1)[0];
            rows += "<a class='chat'>";
            rows += "<span class='chat__id'>" + chat.id + "</span>";
            rows += "<div class='chat__icon'></div>";
            rows += "<div class='chat__container'>";
            rows += "<span class='chat__name'>" + chat.name + "</span></br>";
            rows += "<span class='chat__message'>" + message.text + "</span></br>";
            //rows += "<span class='chat__date'>" + chat.message.date + "</span>";
            rows += "</div>";
            rows += "</a>";
        });
        $(rows).click(function () {
            let chatId = $(this).find(".chat__id").text();
            sidebarViewManager.onChatClick(chatId);
        }).prependTo(".sidebar>.chats");
    }

    model.onMessageReceive = function (chatId, message) {
        sidebarViewManager.updateChatOnNewMessage(chatId, message);
    }
    $(".sidebar .chat-creation .btn").click(function () {
        sidebarViewManager.onCreateDirectChatClick();
    });

    return sidebarViewManager;
}

function MainViewManager(model) {
    var mainViewManager = {
        currentChatId: null,
        showChat(chatId) {
            var chat = model.getChat(chatId);
            mainViewManager.currentChatId = chat.id;
            $(".chat-details > .chat-details__name").text(chat.name);
            $(".main > input").removeClass("d-none").val("");

            var messages = model.getMessages(chat.id, null);
            $(".main .messages").empty();
            mainViewManager.addMessages(messages);
        },

        //выводит сообщения в текущий чат
        //messages: сообщения отсортированные от самого нового к самому старому
        addMessages(messages) {
            let rows = "";
            messages = messages.reverse();
            $.each(messages, function (i, message) {
                let user = model.getUser(message.senderId);
                rows += "<div class='message'>";
                rows += "<div class='message__icon'></div>";
                rows += "<div class='message__container'>";
                rows += "<div class='message__user-name'>" + user.name + "</div>";
                rows += "<div class='message__text'>" + message.text + "</div>";
                rows += "</div>";
                rows += "</div>";
            });

            $(".main .messages").append(rows);

            var mainBox = $(".main .main__box")[0];
            mainBox.scrollTop = mainBox.scrollHeight;
        }
    }

    $(".main>input").keydown(function (event) {
        if (event.keyCode == 13) {
            model.sendMessage(mainViewManager.currentChatId, this.value);
            this.value = "";
            return false;
        }
        return true;
    });

    model.onMessageReceive = function (chatId, message) {
        if (mainViewManager.currentChatId == chatId)
            mainViewManager.addMessages([message]);
    }

    return mainViewManager;
}

function CreateDirectChatViewManager(model) {
    var createDirectChatViewManager = {
        onOpenChatClick(chatId) { },

        Show() {
            var modal = $(".modal");
            modal.load("/Chat/CreateDirectChat", function () {
                modal.modal('show');

                $("#direct-chat-creation form input[type='button']").click(function () {
                    var userId = $("#direct-chat-creation form input[type='text']").val();
                    var user = model.getUser(userId);
                    var foundUserPlace = $("#direct-chat-creation .found-user");
                    let rows = "";

                    if (user == null || user.IsCurrentUser == true) {
                        rows += "<span>Пользователь не найден</span>";
                        foundUserPlace.attr("data-state", "is-not-found");
                    }
                    else {
                        rows += "<div>";
                        rows += "<span>Логин: " + user.login + "</span></br>";
                        rows += "<span>Имя: " + user.name + "</span>";
                        rows += "</div>";
                        rows += "<input type='button' class='btn btn-primary' value='Диалог'/>";
                        foundUserPlace.attr("data-state", "");
                    }

                    foundUserPlace.html(rows).find("input")
                        .click(function () {
                            var chatId = model.getDirectChatId(userId)
                            modal.modal("hide")
                            createDirectChatViewManager.onOpenChatClick(chatId);
                        });
                });
            });
        }
    }

    return createDirectChatViewManager;
}

const model = new Model();
const sidebarViewManager = new SidebarViewManager(model);
const mainViewManager = new MainViewManager(model);
const createDirectChatViewManager = new CreateDirectChatViewManager(model);

sidebarViewManager.onChatClick = mainViewManager.showChat;
sidebarViewManager.onCreateDirectChatClick = createDirectChatViewManager.Show;
createDirectChatViewManager.onOpenChatClick = mainViewManager.showChat;

$(document).ready(function () {
    sidebarViewManager.showChats();
});
