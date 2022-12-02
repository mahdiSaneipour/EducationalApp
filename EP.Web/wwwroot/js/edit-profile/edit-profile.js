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

        var previousAvatar = previousChosedImage.value;

        let avatarModel = new FormData();

        console.log(previousChosedImage.value)

        avatarModel.set("avatar", blob);
        avatarModel.set("previousAvatar", previousAvatar);

        var xhr = new XMLHttpRequest();

        xhr.responseType = 'json';
        xhr.open('POST', '/UserPanel/Home/UploadAvatarAndDeletePreviousOne', true);
        xhr.send(avatarModel);

        xhr.onreadystatechange = async function () {

            if (this.readyState == 4 && this.status == 200) {

                console.log("avatar address : " + this.response.avatarAddress)
                console.log("status : " + this.response.status)

                userAvatar.value = this.response.avatarAddress;

                previousChosedImage.value = this.response.avatarAddress;

                document.getElementById("image").src = "/images/profile-images/" + this.response.avatarAddress;

                cropper.destroy();
                image = null;
                file.value = null;

            }


        }
    })

})

