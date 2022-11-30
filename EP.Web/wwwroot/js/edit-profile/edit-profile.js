var image = document.getElementById("image")
var file = document.getElementById("file")
var saveProfile = document.getElementById("save-profile")
let cropper;

image.addEventListener("click", function () {
    file.click();
})

file.addEventListener("change", function (event) {
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

        let avatarModel = new FormData();
        avatarModel.set("avatar", blob);
        avatarModel.set("previousAvatar","NoPreviousAvatar");

        var xhr = new XMLHttpRequest();

        /*var avatarModel = { PreviousSelectedAvatar: "NoPreviousAvatar", SelectedAvatarFile: avatar }*/

        xhr.responseType = 'json';
        xhr.open('POST', '/UserPanel/Home/UploadAvatarAndDeletePreviousOne', true);
        xhr.send(avatarModel);

        xhr.onreadystatechange = function () {

            if (this.readyState == 4 && this.status == 200) {

                console.log("avatar address : " + this.response.avatarAddress)
                console.log("status : " + this.response.status)

            }


        }
    })

})

