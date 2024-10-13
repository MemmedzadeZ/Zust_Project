function GetAllUsers() {
    $.ajax({
        url: "Home/GetAllUsers",
        method: "GET",
        success: function (data) {
            console.log(data);
            let content = "";

            for (var i = 0; i < data.length; i++) {
                const style = data[i].isOnline ? 'status-online' : 'status-offline';

                const item = `
                    <div class="card" style="${style}; width:300px; margin-top:50px; margin-right:30px">
                        <img style="width:100%; height:250px;" src="/images/${data[i].image}" />
                        <div class="card-body">
                            <h5 class="card-title">${data[i].userName}</h5>
                            <p class="card-text">${data[i].email}</p>
                            <button class="btn btn-primary follow-btn" data-id="${data[i].id}" onclick ="SendFollow('${data[i].id}')">
                                Follow
                            </button>
                        </div>
                    </div>`;
                content += item;
            }

            $("#allUsers").html(content);

            // Attach event listeners for follow buttons
            $(".follow-btn").on("click", function () {
                const userId = $(this).data("id");
                sendFriendRequest(userId);
            });
        }
    });
}

GetAllUsers();
GetMyRequests();

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
                    <button class='btn btn-secondary'>Decline</button>

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
function sendFriendRequest(receiverId) {
    $.ajax({
        url: `/Home/SendRequest/?receiverId=${receiverId}&senderId=${senderId}`,
        method: 'POST',
        success: function (response) {
            let content = "";
            for (var i = 0; i < response.messages.length; i++) {
                let dateTime = new Date(response.messages[i].dateTime);
                let hour = dateTime.getHours();
                let minute = dateTime.getMinutes();
                let item = '';
                if (response.messages[i].receiverId == response.currentUserId) {
                    item = `<section style="display:flex;margin-top:25px;border:2px solid black;
    margin-left:10px;border-radius:10px;background-color:lightgrey;min-width:20%;max-width:50%;">

                                            <h5 style="margin-left:10px;margin-top:15px;margin-right:10px;font-size:1em;">${data.messages[i].content}</h5>
                                            <p style="margin-top:20px;margin-right:10px;font-size:0.9em">${hour}:${minute}</p>
                                        
                                        </section>`;

                }
                else {
                    item = `<section style="display:flex;margin-top:25px;border:2px solid black;
    margin-left:50%;border-radius:10px;background-color:blue;min-width:20%;max-width:50%;">

                                            <h5 style="margin-left:10px;margin-top:15px;margin-right:10px;font-size:1em;color:white">${data.messages[i].content}</h5>
                                            <p style="margin-top:20px;margin-right:10px;font-size:0.9em;color:white">${hour}:${minute}</p>
                                        
                                        </section>`;
                }
                content += item;
            }
            console.log(data);
            $("#currentMessages").html(content);
        },
        error: function (error) {
            console.error('Error occurred:', error);
        }
    });
}

setInterval(GetAllUsers, 1000);
