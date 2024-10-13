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
                                </div>
                                <div class='add-friend-btn'>
                        <a class='btn btn-outline-secondary' href='/Home/GoChat' >Go Chat</a>
                                </div>`;
                }
                else {
                    if (data[i].isFriend) {
                        subContent = `
                         <div class='add-friend-btn'>
                        <button class='btn btn-outline-secondary' onclick="UnFollowRequest('${data[i].id}')">UnFollow</button>
                                </div>
                                `;
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

function addNewPost() {


    $.ajax({
        url: "/Home/GetCreatePostFormViewComponent",
        type: "GET",
        success: function (component) {
            var postArea = document.getElementById('postArea');
            postArea.innerHTML = "";
            postArea.innerHTML += component;




            $.ajax({
                url: "/Home/GetPosts",
                type: "GET",
                success: function (posts) {
                    var postHtml = "";
                    for (let i = 0; i < posts.length; i++) {
                        console.log(posts[i].id);
                        postHtml += `
        <div class="news-feed news-feed-post">
            <div class="post-header d-flex justify-content-between align-items-center">
                <div class="image">
                    <a href="my-profile.html"><img src={${posts[i].user.userProfileImage}} class="rounded-circle" alt="image"></a>
                </div>
                <div class="info ms-3">
                    <span class="name"><a href="my-profile.html">${posts[i].user.userName}</a></span>
                    <span class="small-text"><a href="#">${posts[i].timeAgo}</a></span>
                </div>
${posts[i].isCurrentUser ? `                <div class="dropdown">
                    <button class="dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="flaticon-menu"></i></button>
                    <ul class="dropdown-menu">
                        <li><a class="dropdown-item d-flex align-items-center" href="#"><i class="flaticon-edit"></i> Edit Post</a></li>
                        <li><a class="dropdown-item d-flex align-items-center" href="#"><i class="flaticon-trash"></i> Delete Post</a></li>
                    </ul>
                </div>`: ''}
            </div>

            <div class="post-body">
                <p>${posts[i].message}</p>
            ${posts[i].postImage ? `
            <div class="post-image">
                <img src="${posts[i].postImage}" alt="image">
            </div>` : ''}
                <ul class="post-meta-wrap d-flex justify-content-between align-items-center">
                    <li class="post-react">
                   <a href="javascript:void(0)" class="like-button ${posts[i].isLiked ? 'liked' : ''}" data-post-id="${posts[i].postId}" onclick="toggleLike(${posts[i].postId})">
    <i class="${posts[i].isLiked ? 'flaticon-liked' : 'flaticon-like'}"></i><span>${posts[i].isLiked ? 'Liked' : 'Like'}</span>
    <span class="number">${posts[i].likeCount}</span>
</a>
                    </li>
                    <li class="post-comment">
                        <a href="#"><i class="flaticon-comment"></i><span>Comment</span> <span class="number">0 </span></a>
                    </li>
                    <li class="post-share">
                        <a href="#"><i class="flaticon-share"></i><span>Share</span> <span class="number">0 </span></a>
                    </li>
                </ul>
                <div class="post-comment-list" id="commentsSection_${posts[i].postId}">
                    ${getRecentComments(posts[i].postId)}
                </div>

                <form class="post-footer" >
                    <div class="footer-image">
                        <a href="#"><img src="assets/images/user/user-1.jpg" class="rounded-circle" alt="image"></a>
                    </div>
                    <div class="form-group">
                        <textarea id="commentText_${posts[i].postId}" class="form-control" placeholder="Write a comment..."></textarea>
                        <label><a href="#"><i class="flaticon-photo-camera"></i></a></label>
                    </div>
                    <button type="button" onclick="submitComment(${posts[i].id})" class="btn btn-primary">Submit</button>
                </form>
            </div>
        </div>`;
                    }
                    postArea.innerHTML += postHtml;
                }

            });
        }
    });
}


function submitPost() {
    var formData = new FormData(document.getElementById('postForm'));

    $.ajax({
        url: '/Home/CreatePost',
        type: 'POST',
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            if (response.success) {
                //addNewPost(response);
                $('#postForm')[0].reset();
                console.log(response.message);
            } else {
                console.log("Error occurred");
            }

            addNewPost();

        },
        error: function (xhr, status, error) {

            console.error(error);
        }
    });
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
