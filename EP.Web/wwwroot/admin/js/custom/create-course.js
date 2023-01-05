$("#group").change(function () {

    $("#sub-group").empty();

    $.getJSON("/Api/Admin/GetCourseGroupsByParentId/" + $("#group :selected").val(), function(data) {
        $.each(data, function() {
            var option = document.createElement("option");

            option.value = this.value;
            option.text = this.text;

            $("#sub-group").append(option);
        });
    });
});

// upload image course

var image = document.getElementById("image")
var file = document.getElementById("file")
var previousChosedImage = document.getElementById("previous-selected-avatar")
var saveProfile = document.getElementById("save-profile")
var cropperBg = document.getElementsByClassName("cropper-bg")[0]
var userAvatar = document.getElementById("user-avatar")
let cropper;

image.addEventListener("click", function () {
    file.click();
})

file.addEventListener("change", function (event) {

    var extFile = file.value.substr(file.value.lastIndexOf(".") + 1, file.value.length).toLowerCase();

    if (extFile == "jpg" || extFile == "jpeg" || extFile == "png") {


        image = document.getElementById("image")

        let file = event.target.files[0];
        let reader = new FileReader();

        reader.addEventListener("load", function () {

            /*previousProfile = image.getAttribute("src");*/

            image.src = reader.result;
        }, false)

        if (file) {
            reader.readAsDataURL(file);
        }

    } else {
        alert("فقط میتوانید عکس انتخاب کنید");
    }
})

image.addEventListener("load", function () {


    cropper = new Cropper(image, {
        aspectRatio: 1 / 1
    });

})

saveProfile.addEventListener("click", function () {

    cropper.getCroppedCanvas({

    }).toBlob(function (blob) {

        var previousCourseImage = previousChosedImage.value;

        let avatarModel = new FormData();

        console.log(previousChosedImage.value)

        avatarModel.set("avatar", blob);
        avatarModel.set("previousCourseImage", previousCourseImage);

        var xhr = new XMLHttpRequest();

        xhr.responseType = 'json';
        xhr.open('POST', '/Api/Admin/UploadImageCourseAndDeletePreviousOne', true);
        xhr.send(avatarModel);

        xhr.onreadystatechange = async function () {

            if (this.readyState == 4 && this.status == 200) {

                userAvatar.value = this.response.avatarAddress;

                previousChosedImage.value = this.response.avatarAddress;
                document.getElementById("image").src = "/images/course-images/normal-size/" + this.response.avatarAddress;

                cropper.destroy();
                image = null;
                file.value = null;

            }
        }
    })

})

