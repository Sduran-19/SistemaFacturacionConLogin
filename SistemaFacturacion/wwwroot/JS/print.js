function printPage() {
    var content = document.getElementById("pdf-section").innerHTML; // Obtiene solo el contenido que deseas imprimir
    var originalContent = document.body.innerHTML; // Guarda el contenido original de la página

    // Guarda el estilo actual de la página para poder restaurarlo después de imprimir
    var originalStyle = document.body.style.cssText;

    // Aplica un estilo para la impresión (ajusta el tamaño del logo)
    var printStyle = '<style>@media print { .logo-small { width: 150px !important; } }</style>';

    document.body.innerHTML = content + printStyle; // Reemplaza el contenido con la sección a imprimir más los estilos
    window.print(); // Llama a la función de impresión del navegador

    document.body.innerHTML = originalContent; // Restaura el contenido original después de imprimir
    document.body.style.cssText = originalStyle; // Restaura los estilos originales
    location.reload(); // Recarga la página para evitar errores visuales

    function printPDF() {
    const { jsPDF } = window.jspdf;
    const doc = new jsPDF();

    doc.html(document.querySelector("#pdf-section"), {
        callback: function (doc) {
            // Guarda el archivo PDF
            doc.save("factura.pdf");
        },
        x: 10,
        y: 10
    });
}

}
