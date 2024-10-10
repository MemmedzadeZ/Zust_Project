sendFriendRequest();
setInterval(GetAllUsers(), 1000);
function GetAllUsers() {
   
    $.ajax({
        url: "Home/GetAllUsers",
        method: "GET",
        success: function (data) {

            console.log("signalR");
            console.log(data);
            let content = "";
            for (var i = 0; i < data.length; i++) {
                let style = data[i].isOnline ? 'status-online' : 'status-offline';
                const item = `
               <div class="card" style="${style};width:300px;margin-top:50px;margin-right:30px">

                        <img style="width:100%;height:250px;" src="/images/${data[i].image}" />
                        <div class="card-body">
                            <h5 class="card-title">${data[i].userName}</5>
                            <p class="card-text">${data[i].email} </p>
                            ${subContent}
                        </div>

                    </div>`;
                content += item;
            }
            $("#allUsers").html(content);
        }
        
    });
}


GetAllUsers();


function sendFriendRequest(receiverId) {
    $.ajax({
        url: '/Friend/SendRequest',
        method: 'POST',
        data: { receiverId: receiverId },
        success: function (response) {
            console.log('Teklif gönderildi:', response);
        },
        error: function (error) {
            console.error('Xeta var:', error);
        }
    });
}


$(document).ready(function () {
    $('#uploadForm').on('submit', function (e) {
        e.preventDefault();

        var formData = new FormData();
        formData.append("description", $('#message').val());

        var imageFile = $('#imageUpload')[0].files[0];
        var videoFile = $('#videoUpload')[0].files[0];

        if (imageFile) {
            formData.append("formFileImage", imageFile);
        }

        if (videoFile) {
            formData.append("formFileVideo", videoFile);
        }

        $.ajax({
            url: '/Profile/CreatePost',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {

                $('#posts').prepend(response);
                $('#uploadForm')[0].reset();

                alert("Post uploaded successfully!");
            },
            error: function (xhr, status, error) {
                alert("Error uploading post.");
                console.error("AJAX error:", error);
            }
        });
    });

    // sekil veya video yuklendiyinde gelen div
    $('#imageUpload').change(function () {
        var file = this.files[0];
        if (file) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#mediaContainer').append('<div class="media-item"><img src="' + e.target.result +
                    '" alt="Selected Image" style="max-width: 300px;"/><button type="button" style="background-color:#3644D9;color:white;border-radius:20px;border-color:transparent" class="remove-media">x</button></div>');
            }
            reader.readAsDataURL(file);
        }
    });

    $('#videoUpload').change(function () {
        var file = this.files[0];
        if (file) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#mediaContainer').append('<div class="media-item"><video src="' + e.target.result + 
                '" controls style="max-width: 300px;"></video><button type="button" style="background-color:#3644D9;color:white;border-radius:20px;border-color:transparent" class="remove-media">x</button></div>');
            }
            reader.readAsDataURL(file);
        }
    });

    // Medya silme
    $(document).on('click', '.remove-media', function () {
        $(this).parent('.media-item').remove();
    });
});

// Tag elave etmek
function addTag(tag) {
    var messageBox = $('#message');
    var currentMessage = messageBox.val();
    messageBox.val(currentMessage + ' ' + tag);
}

//sekil yuklemek
function triggerFileUpload() {
    document.getElementById('imageUpload').click();

    document.getElementById('imageUpload').onchange = function () {
        document.getElementById('imageUploadForm').submit();
    };
}
//profil melumatlarini sifirlamaq
function confirmDeletion() {
    if (confirm("Are you sure you want to delete your profile information?")) {
        var form = document.getElementById('delete-form');
        var formData = new FormData(form);

        fetch(form.action, {
            method: 'POST',
            body: formData,
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    document.querySelectorAll('.information-list li').forEach(function (item) {
                        item.innerHTML = '<span></span>';
                    });

                    alert("Profil information deleted succesfuly!");

                } else {
                    alert("Error " + data.message);
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert("Something wrong");
            });
    }
}




//$(document).ready(function () {
//    GetAllUsers();
//});


//profil melumati elave etmek
$("#profileInfoForm").on("submit", function (event) {
    event.preventDefault();
    $.ajax({
        url: '/Profile/ProfileInfo',
        type: 'POST',
        data: $(this).serialize(),
        success: function (response) {
            if (response.success) {
                alert("Profile updated successfully!");
            } else {
                alert("Failed to update profile: " + response.errors.join(", "));
            }
        },
        error: function (xhr, status, error) {
            console.error("Submission failed:", error);
        }
    });
});
$("#accountSettingForm").on("submit", function (event) {
    event.preventDefault();
    $.ajax({
        url: '/Profile/AccountSetting',
        type: 'POST',
        data: $(this).serialize(),
        success: function (response) {
            if (response.success) {
                alert("Profile updated successfully!");
            } else {
                alert("Failed to update profile: " + response.errors.join(", "));
            }
        },
        error: function (xhr, status, error) {
            console.error("Submission failed:", error);
        }
    });
});
//passsword deyismek
$("#changePasswordForm").on("submit", function (event) {
    event.preventDefault();
    $.ajax({
        url: '/Profile/ChangePassword',
        type: 'POST',
        data: $(this).serialize(),
        success: function (response) {
            if (response.success) {
                alert("Profile uptaded successfully!");
            } else {
                alert("Failed to updated profile: " + response.errors.join(", "));
            }
        },
        error: function (xhr, status, error) {
            console.error("Submission failed:", error);
        }
    });
});
//about me
$("#aboutMeForm").on("submit", function (event) {
    event.preventDefault();
    $.ajax({
        url: '/Profile/UpdateAboutMe',
        type: 'POST',
        data: $(this).serialize(),
        success: function (response) {
            if (response.success) {
                alert("Change password successfully!");
            } else {
                alert("Failed to Change password: " + response.errors.join(", "));
            }
        },
        error: function (xhr, status, error) {
            console.error("Submission failed:", error);
        }
    });
});



//hide profil info
document.getElementById("toggle-info").addEventListener("click", function () {
    var infoList = document.getElementById("personal-info");
    if (infoList.style.display === "none") {
        infoList.style.display = "block";
        this.textContent = "Hide Information";
    } else {
        infoList.style.display = "none";
        this.textContent = "Show Information";
    }
});
//delete post 
$(document).on("click", ".delete-post", function () {
    var postId = $(this).data("id");

    if (confirm("Are you sure you want to delete this post?")) {
        $.ajax({
            url: '/Profile/DeletePost',
            type: 'POST',
            data: { id: postId },
            success: function (response) {
                if (response.success) {
                    $("#post-" + postId).remove();
                    alert("Post deleted successfully!");
                } else {
                    alert("Error: " + response.message);
                }
            },
            error: function (xhr, status, error) {
                console.error("Delete failed:", error);
            }
        });
    }
});

// Hide Post
$(document).on("click", ".hide-post", function () {
    var postId = $(this).data("id");
    var btn = $(this);

    $.ajax({
        url: '/Profile/HidePost',
        type: 'POST',
        data: { id: postId },
        success: function (response) {
            if (response.success) {
                alert("Post status updated successfully!");
                if (btn.text() == "Hide Post") {
                    btn.text("Show Post");
                    btn.find('i').removeClass('flaticon-private').addClass('flaticon-public'); 
                }
                else {
                    btn.text("Hide Post");
                    btn.find('i').removeClass('flaticon-private').addClass('flaticon-private'); 
                }
            } else {
                alert("Error: " + response.message);
            }
        },
        error: function (xhr, status, error) {
            console.error("Hide failed:", error);
        }
    });
});


//edit post
function editPost(postId) {
    $.ajax({
        url: '/Profile/EditPost',
        type: 'GET',
        data: { id: postId },
        success: function (response) {
            if (response.success) {
                $('#message').val(response.data.message);

                $('#mediaContainer').html(''); // evvelki medyayı temizle
                if (response.data.imageUrl) {
                    $('#mediaContainer').append(`<img src="${response.data.imageUrl}" alt="Media" style="max-width: 300px;"/>`);
                }

                if (response.data.videoUrl) {
                    $('#mediaContainer').append('<video controls><source src="' + response.data.videoUrl + '" type="video/mp4" /></video>');
                }

                // Movcud postu edit etmek ucun form
                $('#uploadForm').data('id', postId); // Post Id-sini formda saxla
            }
        },

        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
//post refresh
$('#uploadForm').on('submit', function (e) {
    e.preventDefault();

    var formData = new FormData();
    var postId = $('#uploadForm').data('id'); // Post ID-si

    formData.append("id", postId); // Post ID'sini formData'ya ver
    formData.append("description", $('#message').val());

    var imageFile = $('#imageUpload')[0].files[0];
    var videoFile = $('#videoUpload')[0].files[0];

    if (imageFile) {
        formData.append("formFileImage", imageFile);
    }

    if (videoFile) {
        formData.append("formFileVideo", videoFile);
    }

    $.ajax({
        url: '/Profile/UpdatePost', 
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.success) {
                // Yenilenen postu ui-da goster
                $('#postContainer').replaceWith(response);
                alert("Post updated successfully!");
            } else {
                alert("Error updating post: " + response.message);
            }
        },
        error: function (xhr, status, error) {
            console.error("Update failed:", error);
        }
    });
});
