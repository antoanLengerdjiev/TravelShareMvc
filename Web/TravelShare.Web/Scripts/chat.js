$.connection.hub.start();

var chat = $.connection.chat;
var room = $("#chatId").val()
var chatBox = $('#chat-history');
chatBox.scrollTop(chatBox[0].scrollHeight);
setTimeout(function () { chat.server.joinRoom(room) }, 1000);

var $buttonSendMsg = $('#send-message-to-room');
var $msgInput = $('#room-message');

$buttonSendMsg.click(function () {

    var msg = $msgInput.val();
    $msgInput.val("").focus();;
    if (!!msg) {
        chat.server.sendMessageToRoom(msg, room);
    }
    
});

var $buttonLeaveTrip = $("#leaveTrip")
$buttonLeaveTrip.click(function () {
    $.connection.hub.stop();
});

chat.client.addMessage = addMessage;
chat.client.connectUser = function (name) {
    var encodedName = $('<div />').text(name).html();
    var img = $('<img/>');
    img.addClass('online-user-icon');
    img.attr('src', '/Content/Images/online_user_icon.png');
    var li = $('<li id="' + encodedName + '">')
    var divName = $('<span>' + encodedName + '</span>');
    li.append(img);
    li.append(divName);
    $('#chat-online-users').append(li);
   // $('#chat-online-users').append('<li id="' + encodedName + '"><div>' + encodedName + '</div></li>');
}

chat.client.disconnectUser = function (name) {
    $(document.getElementById(name)).remove();
}

function addMessage(message, sender) {

    var li = $('<li>').addClass("clearfix");
    var msgData = $('<div>').addClass("message-data");
    var spanName = $('<span>').add('message-data-name');
    var icon = $('<i>').addClass('fa fa-circle you');
    var divMessage = $('<div>').addClass('message').html(message);
    var curUsername = $('#currentUsername').val();

    if (sender == curUsername) {

        divMessage.addClass('you-message');
        icon.addClass('you');
        spanName.html('You')


    } else {
        divMessage.addClass('me-message');
        icon.addClass('me');
        spanName.html(sender)
        msgData.addClass('align-right')
    }

    msgData.append(icon);
    msgData.append(spanName);

    li.append(msgData);
    li.append(divMessage);

    $('#messages').append(li);
    chatBox.scrollTop(chatBox[0].scrollHeight);
}

$('#chat-history').scroll(function () {
    if ($('#chat-history').scrollTop() == 0) {
        var skip = $('#messages').children().length;
        console.log(skip);

        $.ajax({
            url: "/Trip/GetChatHistory",
            data: { id: room, skip }, // This line shows sending the data.  How you get it is up to you
            dataType: 'html',
            success: function (data) {
                $('#messages').prepend(data);
                $('#chat-history').scrollTop(30); // Scroll alittle way down, to allow user to scroll more
            }
        })
    }
});

