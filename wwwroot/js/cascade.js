$(document).ready(function () {
   // GetCountry();
    $('#stateSelect').attr('disabled', true);
    $('#citySelect').attr('disabled', true);

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