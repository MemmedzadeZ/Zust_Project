function GetAllUsers() {
    $.ajax({
        url: "/Home/GetAllUsers",
        method: "GET",
        success: function (data) {


            let subContent = '';
            console.log("heloooo")
            for (let i = 0; i < data.length; i++) {
                let style = '';

                var usersListDiv = document.getElementById("users-list");
                var status = (data[i].isOnline) ? "online" : "offline";
                if (data[i].hasRequestPending) {
                    subContent = `
                     <div class='add-friend-btn'>
                    <button class='btn btn-outline-secondary' onclick="TakeRequest('${data[i].id}')">Already Sent</button>
                                </div>`;
                }
                else {
                    if (data[i].isFriend) {
                        subContent = `
                         <div class='add-friend-btn'>
                        <button class='btn btn-outline-secondary' onclick="UnFollowRequest('${data[i].id}')">UnFollow</button>
                                </div>
                                <div class='send-message-btn'>
                        <a class='btn btn-outline-secondary m-2' href='/Home/GoChat/${data[i].id}' >Send Message</a>
                                </div>`;
                    }
                    else {
                        subContent = `
                         <div class='add-friend-btn'>
                        <button class='btn btn-outline-primary' onclick="SendFollow('${data[i].id}')">Follow</button>          
                                </div>
                        `;
                    }
                }


                var content = "";


                let item = `
             <div class='col-lg-3 col-sm-6'>
                <div class='single-friends-card'>
                        <div class='friends-image'>
                            <a href='#'>
                                <img src='~/assets/images/friends/friends-bg-1.jpg' alt='image'>
                            </a>
                            <div class='icon'>
                                <a href='#'><i class='flaticon-user'></i></a>
                            </div>
                        </div>
                        <div class='friends-content'>
                            <div class='friends-info d-flex justify-content-between align-items-center'>
                                <a href='#'>
                                    <img src="js/images/user/user.png" alt='image'>
                                </a>
                                <div class='text ms-3'>
                                    <h3><a href='#'>${data[i].userName}</a></h3>
                                    <span>${status}</span>
                                </div>
                            </div>
                            <ul class='statistics'>
                                <li>
                                    <a href='#'>
                                        <span class='item-number'>862</span>
                                        <span class='item-text'>Likes</span>
                                    </a>
                                </li>
                                <li>
                                    <a href='#'>
                                        <span class='item-number'>91</span>
                                        <span class='item-text'>Following</span>
                                    </a>
                                </li>
                                <li>
                                    <a href='#'>
                                        <span class='item-number'>514</span>
                                        <span class='item-text'>Followers</span>
                                    </a>
                                </li>
                            </ul>
                            <div class='button-group d-flex justify-content-between align-items-center'>
                               
                                   ${subContent}
                             
                            </div>
                        </div>
                </div>
             </div>
                   `;
                content += item;
            }
            $("#users-list").html(content);
        }

    })

}

GetMyRequests();
GetAllUsers();

function UnFollowRequest(id) {
    const element = document.querySelector("#alert");
    element.style.display = "none";
    $.ajax({
        url: `/Home/Unfollow?Id=${id}`,
        method: "DELETE",
        success: function (data) {
            element.style.display = "block";
            element.innerHTML = "Your unfollow your friend";
            GetAllUsers();
            SendFollowCall();
            setTimeout(() => {
                element.innerHTML = "";
                element.style.display = "none";

            }, 5000)
        }

    })
}

function TakeRequest(id) {
    const element = document.querySelector("#alert");
    element.style.display = "none";
    $.ajax({
        url: `/Home/TakeRequest?Id=${id}`,
        method: "DELETE",
        success: function (data) {
            element.style.display = "block";
            element.innerHTML = "Your friend request sent successfully";
            GetAllUsers();
            SendFollowCall();
            setTimeout(() => {
                element.innerHTML = "";
                element.style.display = "none";

            }, 5000)
        }

    })
}
function SendFollow(id) {
    const  element = document.querySelector("#alert");
    element.style = "none";
    $.ajax({
        url: `/Home/SendFollow/${id}`,
        method: "GET",
        success: function (data) {
            element.style.display = "block";
            element.innerHTML = "Your friend request sent successfully";
            GetAllUsers();
            SendFollowCall();
            setTimeout(()=>
                {
                    element.innerHTML = "";
                    element.style.display = "none";
                   
                },5000)
        }
    })

}
function DeclineRequest(id, senderId) {
    $.ajax({
        url: `/Home/DeclineRequest?id=${id}&senderId=${senderId}`,
        method: 'GET',
        success: function () {
            element.style.display = "block";
            element.innerHTML = "You decline request";
            SendFollowCall(senderId);
            GetAllUsers();
            GetMyRequests();
            setTimeout(() => {
                element.innerHTML = "";
                element.style.display = "none";

            }, 5000)
        }
    })
}
function GetMyRequests() {
    $.ajax({
        url: '/Home/GetAllRequests',
        method: 'GET',
        success: function (data) {
            let content = "";
            let subContent = "";
            for (let i = 0; i < data.length; i++) {
                if (data[i].status == "Request")
                {
                    subContent = `
                    <div class ='card-body'>
                    <button class='btn btn-success'>Accept</button>
                    <button class='btn btn-secondary' onClick="DeclineRequest(${data[i].id},'${data[i].senderId}')">Decline</button>

                    </div>
                    `;
                }
                else{
                    subContent = `
                    <div class ='card-body'>
                   
                    <button class='btn btn-warning'>Delete</button>

                    </div>
                    `;
                }
                let item = `
                <div class='card' style='width:15rem'>
                <div class='card-body'>
                <h5>Request</h5>
                <ul stype = "list-style:none;">
                    <li>${data[i].content}</li>
                </ul>
                ${subContent}
                </div>
                </div>
                `;
                content += item;
            }
            $("#requests").html(content);

        }
    })
}


function GetMessages(receiverId, senderId) {
    var container = document.querySelector("#chat-container");
    $.ajax({

        url: `/Home/GetAllMessages?receiverId=${receiverId}&senderId=${senderId}`,
        method: "GET",
        success: function (data) {
            var chat = ``;
            var list = ``;
            for (var i = 0; i < data.messages.length; i++) {
                if (receiverId == data.currentUserId) {
                    chat = `
                <div class="chat">
                    <div class="chat chat-left">
                        <div class="chat-avatar">
                            <a routerLink="/profile" class="d-inline-block">
                                <img src="~/assets/images/user/user-8.jpg" width="50" height="50" class="rounded-circle" alt="image">
                            </a>
                        </div>

                        <div class="chat-body">
                            <div class="chat-message">
                                <p>${messages[i].content}</p>
                                <span class="time d-block">${messages[i].dateTime}</span>
                            </div>
                        </div>
                    </div>
                </div>`;



                }

                list += chat;

            }
            container.innerHTML = list;

        }

    })
}


setInterval(GetAllUsers, 1000);
