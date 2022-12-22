let courseGroup = ("#Course_GroupId")
let courseSubGroup = ("#Course_SubGroupId")


$("#Course_GroupId").change(function() {
    $("#Course_SubGroupId").empty();

    $.getJSON("/Api/Admin/GetCourseGroupsByParentId/" + $("#Course_GroupId :selected").val(), function(data) {
        $.each(data, function() {
            var option = document.createElement("option");

            option.value = this.value;
            option.text = this.text;

            $("#Course_SubGroupId").append(option);
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
                console.log("pic : " + this.response.avatarAddress);
                document.getElementById("image").src = "/images/course-images/normal-size/" + this.response.avatarAddress;

                cropper.destroy();
                image = null;
                file.value = null;

            }


        }
    })

})

