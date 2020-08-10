var config = {
    language: 'en',
    extraPlugins: 'codesnippet, mathjax, forms, emoji, justify, smiley, font, colorbutton, uicolor, colordialog, tableresize, wysiwygarea ',
    mathJaxLib: 'https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.4/MathJax.js?config=TeX-AMS_HTML',
    filebrowserUploadUrl: '/uploader/upload.php',
    codeSnippet_theme: 'default',
    height: 356
};

CKEDITOR.replace('editor', config);