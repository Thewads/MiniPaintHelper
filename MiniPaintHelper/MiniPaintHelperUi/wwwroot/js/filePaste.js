export function initializeFilePaste() {
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
    function onPaste(event) {
        // use event.originalEvent.clipboard for newer chrome versions
        let items = (event.clipboardData  || event.originalEvent.clipboardData).items;
        let blob = null;
        for (let i = 0; i < items.length; i++) {
            if (items[i].type.indexOf("image") === 0) {
                blob = items[i].getAsFile();
            }
        }
        if (blob !== null) {
            let reader = new FileReader();
            reader.onload = imageIsLoaded
            reader.readAsDataURL(blob);
        }
    }
    
    document.addEventListener('paste', onPaste);

    
    return {
        dispose: () => {
           document.removeEventListener('paste', onPaste);
        }
    }
}