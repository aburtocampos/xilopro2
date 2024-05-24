document.addEventListener('DOMContentLoaded', function () {
    // GetCountry();

    var valorSeleccionadoCountry = $('#countrySelect').val();
    var valorSeleccionadoState = $('#stateSelect').val();
    var valorSeleccionadoCity = $('#citySelect').val();
    var valorModelState = @Model.Stateid;
    var valorModelCity = @Model.Cityid;

    console.log('El valor seleccionado Stateid es ', valorModelState);
    console.log('El valor seleccionado Cityid es ', valorModelCity);
    console.log('El valor seleccionado valorSeleccionadoCountry es ', valorSeleccionadoCountry);

    if (valorSeleccionadoCountry !== '0') {
        $('#stateSelect').attr('disabled', false);
        $('#citySelect').attr('disabled', false);


        $('#stateSelect').append('<option>Seleccionar Departamento...</option>');
        $.ajax({
            url: '/Players/StateDrop?id=' + valorSeleccionadoCountry,
            success: function (result) {
                $.each(result, function (i, data) {
                    var selected = (data.state_ID == valorModelState) ? ' selected' : '';
                    $('#stateSelect').append('<option value="' + data.state_ID + '"' + selected + '>' + data.state_Name + '</option>');
                });
            }
        });


        $('#citySelect').append('<option>Seleccionar Municipio...</option>');
        $.ajax({
            url: '/Players/CityDrop?id=' + valorModelState,
            success: function (result) {
                $.each(result, function (i, data) {
                    var selected = (data.city_ID == valorModelCity) ? ' selected' : '';
                    $('#citySelect').append('<option value="' + data.city_ID + '"' + selected + '>' + data.city_Name + '</option>');
                });
            }
        });



    } else {
        var valorSeleccionadoCountry = $('#countrySelect').val();
        console.log('El valor seleccionado es :', valorSeleccionadoCountry);

        $('#stateSelect').attr('disabled', true);
        $('#citySelect').attr('disabled', true);
    }


    //changes eventos

    $('#countrySelect').change(function () {
        $('#stateSelect').attr('disabled', false);
        var id = $(this).val();
        $('#stateSelect').empty();
        $('#stateSelect').append('<option>Seleccionar Departamento...</option>');
        $.ajax({
            url: '/Players/StateDrop?id=' + id,
            success: function (result) {
                $.each(result, function (i, data) {
                     $('#stateSelect').append('<option value=' + data.state_ID + '>' + data.state_Name + '</option>');
                });
            }
        });
    });


    $('#stateSelect').change(function () {
        $('#citySelect').attr('disabled', false);
        var idd = $(this).val();
        $('#citySelect').empty();
        $('#citySelect').append('<option>Seleccionar Municipio...</option>');
        $.ajax({
            url: '/Players/CityDrop?id=' + idd,
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#citySelect').append('<option value=' + data.city_ID + '>' + data.city_Name + '</option>');
                });
            }
        });
    });






});

/*function GetCountry() {
    $.ajax({
        url: '/Cascade/Country',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#Country').append('<Option value=' + data.id + '>' + data.name + '</Option>');
            });
        }
    });
}*/