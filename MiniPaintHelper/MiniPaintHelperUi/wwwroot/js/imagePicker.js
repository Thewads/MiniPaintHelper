let paintFromImageRef;

function imageIsLoaded(e) {
    const img = new Image();
    img.onload = function(){
        const canvas = $('#canvas_picker')[0];
        const context = canvas.getContext('2d');
        canvas.width  = this.width;
        canvas.height = this.height;
        context.drawImage(this, 0, 0);
    };
    img.src = e.target.result;
}

window.registerImagePicker = function (dotNetObject){
    paintFromImageRef = dotNetObject;
    $("#file_upload").change(function (e) {
        let F = this.files[0];
        let reader = new FileReader();
        reader.onload = imageIsLoaded;
        reader.readAsDataURL(F);
    });

    $('#canvas_picker').click(function(event){
        const canvas = $('#canvas_picker')[0];
        const context = canvas.getContext('2d');

        let x = event.pageX - $(this).offset().left;
        let y = event.pageY - $(this).offset().top;
        let img_data = context.getImageData(x,y , 1, 1).data;
        let R = img_data[0];
        let G = img_data[1];
        let B = img_data[2];

        paintFromImageRef.invokeMethodAsync("ColorPicked", R, G, B);
    });
}





