document.addEventListener("DOMContentLoaded", function () {

    var ruta = '@Url.Content("/Content/Teams/")';
    var teamLocal = document.getElementById('team_local_select');
    var imgLocal = document.getElementById('imgLocal');
    var teamVisitante = document.getElementById('team_visitor_select');
    var imgVisitante = document.getElementById('imgVisitante');

    var flagshowText = false;

    function processSelectedValue(value, imageElement) {

        var parts = value.split('|');
        var teamId = parts[0];
        var imageUrl = parts[1];

        console.log("value->", value);
        console.log("teamId->", teamId);
        console.log("imageUrl->", imageUrl);

        if (value === "0") {
            imageElement.src = '@Url.Content("~/images/noimage.png")';
        } else {
            if (imageUrl != "") {
                imageElement.src = imageUrl ? ruta + imageUrl : '@Url.Content("~/images/noimage.png")';
            } else {

            }

            // imageElement.src = parts.length > 1 ? ruta + imageUrl : '@Url.Content("~/images/noimage.png")';
        }
    }

    teamLocal.addEventListener('change', function () {
        //    if (flagshowText) {
        console.log("teamLocal.options[teamLocal.selectedIndex].text", teamLocal.options[teamLocal.selectedIndex].text)
        // var valorRecuperado = localStorage.getItem('imgData');
        var textParts = teamLocal.selectedOptions[0].textContent.split("|");
        localStorage.setItem('imgData', textParts[1]);
        $(teamLocal.options[teamLocal.selectedIndex]).text(textParts[0]);
        //  processSelectedValue(teamLocal.options[teamLocal.selectedIndex].text, imgLocal)
        //      flagshowText = false;
        //  }
        if (textParts[1] = "") {
            var valorRecuperado = localStorage.getItem('imgData');
            imgLocal.src = valorRecuperado ? ruta + valorRecuperado : '@Url.Content("~/images/noimage.png")';
        }
        if (teamLocal.value === "0") {
            imgLocal.src = '@Url.Content("~/images/noimage.png")';
        } else {
            imgLocal.src = textParts[1] ? ruta + textParts[1] : '@Url.Content("~/images/noimage.png")';
            // imageElement.src = parts.length > 1 ? ruta + imageUrl : '@Url.Content("~/images/noimage.png")';
        }
    });

    /*  teamVisitante.addEventListener('change', function () {
          processSelectedValue(teamVisitante.value, imgVisitante)
      });*/


    if (teamLocal.selectedOptions.length > 0) {
        console.log("teamLocal.selectedOptions[0].textContent", teamLocal.selectedOptions[0].textContent)
        var textParts = teamLocal.selectedOptions[0].textContent.split("|");
        localStorage.setItem('imgData', textParts[1]);
        $(teamLocal.options[teamLocal.selectedIndex]).text(textParts[0]);

        if (teamLocal.value === "0") {
            imgLocal.src = '@Url.Content("~/images/noimage.png")';
        } else {
            imgLocal.src = textParts[1] ? ruta + textParts[1] : '@Url.Content("~/images/noimage.png")';
            // imageElement.src = parts.length > 1 ? ruta + imageUrl : '@Url.Content("~/images/noimage.png")';
        }
        // var index = teamLocal.options[teamLocal.selectedIndex].text.indexOf(char);
        //   var newText = removeCharactersFromString(teamLocal.options[teamLocal.selectedIndex].text, "|");
        //    var newText = removeCharactersFromString(teamLocal.options[teamLocal.selectedIndex].text, "|");
        //  saveimageurlonstorage(teamLocal.selectedOptions[0].textContent, $(teamLocal.options[teamLocal.selectedIndex]))
        //  $(teamLocal.options[teamLocal.selectedIndex]).text(newText);
        //  if (!flagshowText) {  processSelectedValue(teamLocal.selectedOptions[0].textContent, imgLocal); flagshowText = true; }
        //  processSelectedValue(teamLocal.selectedOptions[0].textContent, imgLocal);
    }

    //  if (teamVisitante.selectedOptions.length > 0) {  processSelectedValue(teamVisitante.selectedOptions[0].value, imgVisitante); }

    function removeCharactersFromString(str, char) {
        var index = str.indexOf(char); // Encuentra el índice del primer carácter especificado
        if (index !== -1) {
            return str.substring(0, index); // Retorna la parte de la cadena desde el inicio hasta el índice encontrado
        }
        return str; // Si el carácter no se encuentra, retorna la cadena original
    }

    function saveimageurlonstorage(originalText, selectObj) {
        var textParts = originalText.split("|");

        // Modificar solo la parte después del carácter "|", envolviéndola en un span con estilo CSS para cambiar el color a blanco
        //  textParts[1] = "<span style='color: white;'>" + textParts[1] + "</span>";
        localStorage.setItem('imgData', textParts[1]);

        // Combinar las dos partes nuevamente
        var newText = textParts.join("|");
        console.log("newText", newText)
        // Asignar el nuevo texto a la opción seleccionada
        selectObj.text(newText);

    }



});