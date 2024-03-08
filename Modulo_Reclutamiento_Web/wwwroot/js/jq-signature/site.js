// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.
/*
 */
const valFirma = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAASwAAACWCAYAAABkW7XSAAAAAXNSR0IArs4c6QAABGhJREFUeF7t1IEJADAMAsF2/6EtdIuHywRyBu+2HUeAAIGAwDVYgZZEJEDgCxgsj0CAQEbAYGWqEpQAAYPlBwgQyAgYrExVghIgYLD8AAECGQGDlalKUAIEDJYfIEAgI2CwMlUJSoCAwfIDBAhkBAxWpipBCRAwWH6AAIGMgMHKVCUoAQIGyw8QIJARMFiZqgQlQMBg+QECBDICBitTlaAECBgsP0CAQEbAYGWqEpQAAYPlBwgQyAgYrExVghIgYLD8AAECGQGDlalKUAIEDJYfIEAgI2CwMlUJSoCAwfIDBAhkBAxWpipBCRAwWH6AAIGMgMHKVCUoAQIGyw8QIJARMFiZqgQlQMBg+QECBDICBitTlaAECBgsP0CAQEbAYGWqEpQAAYPlBwgQyAgYrExVghIgYLD8AAECGQGDlalKUAIEDJYfIEAgI2CwMlUJSoCAwfIDBAhkBAxWpipBCRAwWH6AAIGMgMHKVCUoAQIGyw8QIJARMFiZqgQlQMBg+QECBDICBitTlaAECBgsP0CAQEbAYGWqEpQAAYPlBwgQyAgYrExVghIgYLD8AAECGQGDlalKUAIEDJYfIEAgI2CwMlUJSoCAwfIDBAhkBAxWpipBCRAwWH6AAIGMgMHKVCUoAQIGyw8QIJARMFiZqgQlQMBg+QECBDICBitTlaAECBgsP0CAQEbAYGWqEpQAAYPlBwgQyAgYrExVghIgYLD8AAECGQGDlalKUAIEDJYfIEAgI2CwMlUJSoCAwfIDBAhkBAxWpipBCRAwWH6AAIGMgMHKVCUoAQIGyw8QIJARMFiZqgQlQMBg+QECBDICBitTlaAECBgsP0CAQEbAYGWqEpQAAYPlBwgQyAgYrExVghIgYLD8AAECGQGDlalKUAIEDJYfIEAgI2CwMlUJSoCAwfIDBAhkBAxWpipBCRAwWH6AAIGMgMHKVCUoAQIGyw8QIJARMFiZqgQlQMBg+QECBDICBitTlaAECBgsP0CAQEbAYGWqEpQAAYPlBwgQyAgYrExVghIgYLD8AAECGQGDlalKUAIEDJYfIEAgI2CwMlUJSoCAwfIDBAhkBAxWpipBCRAwWH6AAIGMgMHKVCUoAQIGyw8QIJARMFiZqgQlQMBg+QECBDICBitTlaAECBgsP0CAQEbAYGWqEpQAAYPlBwgQyAgYrExVghIgYLD8AAECGQGDlalKUAIEDJYfIEAgI2CwMlUJSoCAwfIDBAhkBAxWpipBCRAwWH6AAIGMgMHKVCUoAQIGyw8QIJARMFiZqgQlQMBg+QECBDICBitTlaAECBgsP0CAQEbAYGWqEpQAAYPlBwgQyAgYrExVghIgYLD8AAECGQGDlalKUAIEDJYfIEAgI2CwMlUJSoCAwfIDBAhkBAxWpipBCRAwWH6AAIGMgMHKVCUoAQIGyw8QIJARMFiZqgQlQMBg+QECBDICBitTlaAECBgsP0CAQEbAYGWqEpQAAYPlBwgQyAgYrExVghIg8ACBlFZdWYR+vQAAAABJRU5ErkJggg==';
var arrBeneficiarios = []

/*
 * ACCIONES AL CARGAR LA PAGINA
**/
$(document).ready(function () {
    ExistenVariables();
    DocumentosFirmados();
    SelectDocumentoActual();

    /*agregar firma de patron, reprecentante legal y testigo */
    $("#firmaPatron").attr('src', sessionStorage.getItem("ReprecentanteLegalFirma"));
    $("#nombrePatron").html(sessionStorage.getItem("ReprecentanteLegal"));
    $("#patronFirmaConvenio").attr('src', sessionStorage.getItem("ReprecentanteLegalFirma"));
    $("#firmaReprecentante").attr('src', sessionStorage.getItem("ReprecentanteLegalFirma"));
    $("#nombreReprecentante").html(sessionStorage.getItem("ReprecentanteLegal"));
    $("#firmaTestigo").attr('src', sessionStorage.getItem("TestigoFirma"));
    $("#nombreTestigo").html(sessionStorage.getItem("Testigo"));

    //sessionStorage.setItem("urlbase",window.location.href)
    //COMPROBAR QUE NO EXISTE CLAVE DE PROSPECTO
    if (sessionStorage.getItem("Clave_Prospecto") === "null") {
        //MOSTRAR MODAL PARA INGRESAR CLAVE PROSPECTO
        $('#modalClaveProspecto').modal('show');
    }
    else if (sessionStorage.getItem("Firma_Prospecto") === "null") {
        $("#modalAvisoPrivacidad").modal('show');
    }


    $('.js-signature').jqSignature({ width: 400, height: 200, lineColor:'4B4B4B'});
    $('#btnLimpiar').click(function ()
    {
        $('.js-signature').jqSignature('clearCanvas');
    });
});

/*
 *  ACCIONES DE MODALES 
 * **/
//clic en modal de ingresar clave prospecto
$("#btn_Aceptar_Clave_Prospecto").click(function () {
    //si la clave ingresada es diferente de null y diferente de vacio, 
    if ($("#mclave_prospecto").val() != null || $("#mclave_prospecto").val() != "") {
        //guardamos la clave
        sessionStorage.setItem("Clave_ProspectoSearch", $("#mclave_prospecto").val());
        //mandamos consultar la clave
        get_Prospecto()
        getFirmas();
        //ocultamos el modal
        $("#modalClaveProspecto").modal('hide');
        if (sessionStorage.getItem("Clave_Prospecto") !== "null") {
            $("#modalAvisoPrivacidad").modal('show');
        }
    }
});

///modal aviso de privacidad

$("#btn_avisoPrivacidad").click(function () {
    //guarda firma
    //sessionStorage.setItem("Firma_Prospecto", $canvas.toDataURL())
    sessionStorage.setItem("Firma_Prospecto", $('.js-signature').jqSignature('getDataURL'))
    console.log(sessionStorage.getItem("Firma_Prospecto"))
    if (sessionStorage.getItem("Firma_Prospecto") === valFirma) {
        alert("Aviso de Privacidad,No tiene Firma");
        console.log(sessionStorage.getItem("Firma_Prospecto"));
    } else {
        sessionStorage.setItem("Documento", "1");
        $('#modalAvisoPrivacidad').modal('hide');
       
        window.location.href = "/Home/Documentos"
        //window.location.href = "/cajerosinteligentesQA/Home/Documentos"   
    }
});


/*
 * 
 * ACCIONES AL FIRMAR
 * */
//firmar doc 1 aviso de candidatos
$("#btn_Av_Candidato").click(function () {
    $("#ModalConfirmacion").modal("show");
});
//firmar Carta_Confidencialidad
$("#btn_Carta_Confidencialidad").click(function () {
    $("#ModalConfirmacion").modal("show");
});
$("#btn_Convenio_Conf").click(function () {
    $("#ModalConfirmacion").modal("show");
});

$("#btn_Av_Privacidad").click(function () {
    $("#ModalConfirmacion").modal("show");
});
$("#btn_Concentimientos").click(function () {
    $("#ModalConfirmacion").modal("show");
});
$("#btn-ContratoLaboral").click(function () {
    $("#ModalConfirmacion").modal("show");
});
//modal de confirmacion
$("#btn_claveConfirmacion").click(function () {
    if ($("#clave_confirmacion").val() !== sessionStorage.getItem("Clave_Prospecto")) {
        alert("Clave Incorrecta")
    } else {

        switch (sessionStorage.getItem("Documento")) {
            case "1":
                $("#firma_AvisoCandidato").attr('src', sessionStorage.getItem("Firma_Prospecto"));
                $("#firma_AvisoCandidato").removeClass("visually-hidden")
                $("#AvNombre_Prospecto").html(sessionStorage.getItem("Nombre_Prospecto"));
                $("#btn_Av_Candidato").addClass("visually-hidden")
                $("#ModalConfirmacion").modal('hide');
                sessionStorage.setItem("Documento", "2")
                sessionStorage.setItem("Doc1_Firmado", "Firmado")
                break;
            case "2":
                var storedArray = JSON.parse(JSON.stringify(arrBeneficiarios));
                if (storedArray.length < 1) {
                    alert("Agrega al menos un Beneficiario")
                    $("#ModalConfirmacion").modal("hide");
                    break;
                } else {
                    var porcentajefinal = 0;
                    for (var i = 0; i < storedArray.length; i++) {
                        porcentajefinal += parseInt(storedArray[i][2]);
                    }
                    if (porcentajefinal > 100) {
                        alert("porcentaje de los bneficiarios supero el 100%");
                        break;
                    } else {
                        sessionStorage.setItem("arrBeneficiarios", JSON.stringify(arrBeneficiarios));
                    }

                }
                /*agregar firma de prospecto */
                $("#firma_ContratoLaboral").attr('src', sessionStorage.getItem("Firma_Prospecto"));
                $("#firma_ContratoLaboral").removeClass("visually-hidden")
                $("#nombre_Contrato").html(sessionStorage.getItem("Nombre_Prospecto"));
                $("#nombre_Contrato2").html(sessionStorage.getItem("Nombre_Prospecto"));
                $("#btn-ContratoLaboral").addClass("visually-hidden")
                $("#ModalConfirmacion").modal('hide');
                sessionStorage.setItem("Documento", "3")
                sessionStorage.setItem("Doc2_Firmado", "Firmado")
                break;
            case '3':
                $("#firmaAvisoPrivacidad").attr('src', sessionStorage.getItem("Firma_Prospecto"));
                $("#firmaAvisoPrivacidad").removeClass("visually-hidden")
                $("#nombre_AvisoPrivacidad").html(sessionStorage.getItem("Nombre_Prospecto"));
                $("#btn_Av_Privacidad").addClass("visually-hidden")
                $("#ModalConfirmacion").modal('hide');
                sessionStorage.setItem("Documento", "4")
                sessionStorage.setItem("Doc3_Firmado", "Firmado")
                break;
            case '4':
                /*-----------------*/
                $("#firmaCartaCandidato").attr('src', sessionStorage.getItem("Firma_Prospecto"));
                $("#firmaCartaCandidato").removeClass("visually-hidden")
                $("#nombre_Carta_Conf").html(sessionStorage.getItem("Nombre_Prospecto"));
                $("#btn_Carta_Confidencialidad").addClass("visually-hidden")
                $("#ModalConfirmacion").modal('hide');
                sessionStorage.setItem("Documento", "5")
                sessionStorage.setItem("Doc4_Firmado", "Firmado")
                break;
            case '5':
                /*-----------*/
                $("#firmaConvenioConfCandidato").attr('src', sessionStorage.getItem("Firma_Prospecto"));
                $("#firmaConvenioConfCandidato").removeClass("visually-hidden")
                $("#nombre_Convenio_Conf").html(sessionStorage.getItem("Nombre_Prospecto"));
                $("#btn_Convenio_Conf").addClass("visually-hidden")
                $("#ModalConfirmacion").modal('hide');
                sessionStorage.setItem("Documento", "6")
                sessionStorage.setItem("Doc5_Firmado", "Firmado")
                break;
            case '6':
                $("#firmaConcentimiento").attr('src', sessionStorage.getItem("Firma_Prospecto"));
                $("#firmaConcentimiento").removeClass("visually-hidden")
                $("#nombre_Concentimientos").html(sessionStorage.getItem("Nombre_Prospecto"));
                $("#nombre_Concentimiento").html(sessionStorage.getItem("Nombre_Prospecto"));
                $("#btn_Concentimientos").addClass("visually-hidden")
                $("#ModalConfirmacion").modal('hide');
                sessionStorage.setItem("Documento", "7")
                sessionStorage.setItem("Doc6_Firmado", "Firmado")
                sessionStorage.setItem("Todo_Firmado", "OK")
                $("#verDocumentos").addClass("visually-hidden")
                $("#guardarDocumentos").removeClass("visually-hidden")
                break;
            default:
                break;

        }

        SelectDocumentoActual()
        DocumentosFirmados()
    }
});
//acciones de documento Contrato laboral
//agregar beneficiarios
$("#addBeneficiario").click(function () {
    
    if (arrBeneficiarios.length < 4) {
        let nombre_beneficiario = prompt("Nombre de Beneficiario");
        if (valNombre(nombre_beneficiario)) {
            let parentesco_beneficiario = prompt("Parentesco")
            if (valParentesco(parentesco_beneficiario)) {
                let porcentaje_beneficiario = prompt("Porcentaje")
                if (valPorcentaje(porcentaje_beneficiario)) {

                    arrBeneficiarios.push([nombre_beneficiario, parentesco_beneficiario, porcentaje_beneficiario]);
                    
                }
            }
        }
    }

    
    add_Beneficiarios();
});

//valida beneficiario
function valNombre(nombre)
{
    if (nombre === null || nombre === '') {
        alert("agregar nombre completo")
        return false;
    } else
    {
        return true;
    }
}
function valParentesco(parentesco) {
    if (parentesco === null || parentesco === '') {
        alert("agregar parentesco")
        return false;
    } else {
        return true;
    }
}
function valPorcentaje(porcentaje) {
    if (!isNaN(porcentaje)) {
        if (porcentaje > 100) {
            alert("porcentaje no debe ser mayor a 100%")
            return false
        } else
        {
            return true;
        }
       
    } else
    {
        alert("agregar porcentaje")
        return false;
    }
}

///llenar beneficiarios

function add_Beneficiarios() {
    $("#beneficiarios tr").remove();
    //var storedArray = JSON.parse(sessionStorage.getItem("arrBeneficiarios"));
    var storedArray = JSON.parse(JSON.stringify(arrBeneficiarios));
    var i;
    for (i = 0; i < storedArray.length; i++) {
        console.log(storedArray[i][0]);
        var tr = $("<tr>");
        tr.attr("id", i)
        var thnombre = $("<th>");
        var spnombre = $("<p>");
        spnombre.text(storedArray[i][0]);
        thnombre.append(spnombre);
        tr.append(thnombre);
        var thparentesco = $("<th>");
        var spparentesco = $("<p>");
        spparentesco.text(storedArray[i][1]);
        thparentesco.append(spparentesco);
        tr.append(thparentesco);
        var thporcentaje = $("<th>");
        var spporcentaje = $("<p>");
        spporcentaje.text(storedArray[i][2]+"%");
        thporcentaje.append(spporcentaje);
        tr.append(thporcentaje);

        var thaccion = $("<th>");
        var btn = $("<button>");
        btn.attr("id", i);
        btn.addClass("btn btn-danger");
        btn.html("Eliminar")
        btn.click(function () {
            $("#beneficiarios tr#" + $(this).attr("id")).remove();
            arrBeneficiarios.splice($(this).attr("id"), 1);
            add_Beneficiarios()
        });
        thaccion.append(btn);

        tr.append(thaccion);
        

        $("#beneficiarios").append(tr);
    }

    //<button id="addBeneficiario" class="btn btn-success">Agregar</button>
}
/**
 *  FUNCIONES
 * **/
function ExistenVariables() {
    if (!sessionStorage.getItem("Id_EmpleadoP")) {
        sessionStorage.setItem("Id_EmpleadoP",0)
    }
    if (!sessionStorage.getItem("Clave_Prospecto")) {
        sessionStorage.setItem("Clave_Prospecto", "null");
    } else {
        $("#dClave_Prospecto").html(sessionStorage.getItem("Clave_Prospecto"));
    }

    if (!sessionStorage.getItem("Nombre_Prospecto")) {
        sessionStorage.setItem("Nombre_Prospecto", "null");
    } else {
        $("#dNombre_Prospecto").html(sessionStorage.getItem("Nombre_Prospecto"));
        $("#nombre_Carta_Conv").html(sessionStorage.getItem("Nombre_Prospecto"));
        $("#nombreConvenio").html(sessionStorage.getItem("Nombre_Prospecto"));
        $("#nombre_contrato").html(sessionStorage.getItem("Nombre_Prospecto"));
        $("#nombre_contrato2").html(sessionStorage.getItem("Nombre_Prospecto"));
        $("#nombre_contrato3").html(sessionStorage.getItem("Nombre_Prospecto"));
    }

    if (!sessionStorage.getItem("Firma_Prospecto")) {
        sessionStorage.setItem("Firma_Prospecto", "null");
    }

    //sexo, edad, estadocivil, curp, rfc, domicilio, puesto

    if (!sessionStorage.getItem("Sexo_Prospecto")) {
        sessionStorage.setItem("Sexo_Prospecto", "null")
    } else {
        $("#sexo").html(sessionStorage.getItem("Sexo_Prospecto"))
    }

    if (!sessionStorage.getItem("Edad_Prospecto")) {
        sessionStorage.setItem("Edad_Prospecto", "null")
    } else {
        $("#edad").html(sessionStorage.getItem("Edad_Prospecto"))
    }
    if (!sessionStorage.getItem("EstadoCivil_Prospecto")) {
        sessionStorage.setItem("EstadoCivil_Prospecto", "null")
    }
    else {
        $("#estadocivil").html(sessionStorage.getItem("EstadoCivil_Prospecto"))
    }
    if (!sessionStorage.getItem("Curp_Prospecto")) {
        sessionStorage.setItem("Curp_Prospecto", "null")
    } else {
        $("#curp").html(sessionStorage.getItem("Curp_Prospecto"));
    }
    if (!sessionStorage.getItem("Rfc_Prospecto")) {
        sessionStorage.setItem("Rfc_Prospecto", "null")
    } else {
        $("#rfc").html(sessionStorage.getItem("Rfc_Prospecto"))
    }
    if (!sessionStorage.getItem("Domicilio_Prospecto")) {
        sessionStorage.setItem("Domicilio_Prospecto", "null")
    } else {
        $("#domicilio").html(sessionStorage.getItem("Domicilio_Prospecto"))
    }
    if (!sessionStorage.getItem("Puesto_Prospecto")) {
        sessionStorage.setItem("Puesto_Prospecto", "null")
    } else {
        $("#puesto").html(sessionStorage.getItem("Puesto_Prospecto"))
    }
    if (!sessionStorage.getItem("Sueldo_Prospecto")) {
        sessionStorage.setItem("Sueldo_Prospecto", "null")
    } else {
        $("#sueldo").html(sessionStorage.getItem("Sueldo_Prospecto"))
    }

    if (!sessionStorage.getItem("Documento")) {
        sessionStorage.setItem("Documento", "null");
    } else {

    }

    //Aviso Candidatos
    if (!sessionStorage.getItem("Doc1_Firmado")) {
        sessionStorage.setItem("Doc1_Firmado", "null");
    } else {

    }

    //Carta Confidencialidad
    if (!sessionStorage.getItem("Doc2_Firmado")) {
        sessionStorage.setItem("Doc2_Firmado", "null");
    } else {
        //agregar firma al documento
    }
    //Convenio de Confidencialidad
    if (!sessionStorage.getItem("Doc3_Firmado")) {
        sessionStorage.setItem("Doc3_Firmado", "null");
    } else {
        //agregar firma al documento
    }
    //Aviso Privacidad
    if (!sessionStorage.getItem("Doc4_Firmado")) {
        sessionStorage.setItem("Doc4_Firmado", "null");
    } else {
        //agregar firma al documento
    }
    //Concentimientos
    if (!sessionStorage.getItem("Doc5_Firmado")) {
        sessionStorage.setItem("Doc5_Firmado", "null");
    } else {
        //agregar firma al documento
    }
    //Contrato Laboral
    if (!sessionStorage.getItem("Doc6_Firmado")) {
        sessionStorage.setItem("Doc6_Firmado", "null");
    } else {
        //agregar firma al documento
    }
    //todo firmado
    if (!sessionStorage.getItem("Todo_Firmado")) {
        sessionStorage.setItem("Todo_Firmado", "null");
    }

    if (!sessionStorage.getItem("arrBeneficiarios")) {
        sessionStorage.setItem("arrBeneficiarios","null")
    }
    
   
}
function get_Prospecto() {

    $.ajax({
        type: "GET",
        url: "/Home/getProspecto/?Clave_prospecto=" + sessionStorage.getItem("Clave_ProspectoSearch"),
        //url: "/cajerosinteligentesQA/Home/getProspecto/?Clave_prospecto=" + sessionStorage.getItem("Clave_ProspectoSearch"),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
            //console.log(data.clave_Prospecto);
            //console.log(data.nombre_Prospecto);
            sessionStorage.setItem("Id_EmpleadoP", data.id_Prospecto)
            $("#dClave_Prospecto").html(data.clave_Prospecto);
            sessionStorage.setItem("Clave_Prospecto", data.clave_Prospecto);
            $("#dNombre_Prospecto").html(data.nombre_Prospecto);
            sessionStorage.setItem("Nombre_Prospecto", data.nombre_Prospecto);
            if (data.sexo_Prospecto === "M") {
                sessionStorage.setItem("Sexo_Prospecto", "MASCULINO");
            } else {
                sessionStorage.setItem("Sexo_Prospecto", "FEMENINO");
            }

            sessionStorage.setItem("Edad_Prospecto", data.edad_Prospecto);
            sessionStorage.setItem("EstadoCivil_Prospecto", data.estadoCivil_Prospecto);
            sessionStorage.setItem("Curp_Prospecto", data.curP_Prospecto);
            sessionStorage.setItem("Rfc_Prospecto", data.rfC_Prospecto);
            sessionStorage.setItem("Domicilio_Prospecto", data.domicilio_Prospecto);
            sessionStorage.setItem("Puesto_Prospecto", data.puesto_Prospecto);
            sessionStorage.setItem("Sueldo_Prospecto", data.sueldo_Prospecto);
            $("#modalAvisoPrivacidad").modal('show');
        },
        error: function (error) {
            $("#dNombre_Prospecto").html("No encontrado");
            sessionStorage.setItem("Clave_Prospecto", "null");
            window.location.href="/Home/Index"
            //window.location.href ="/cajerosinteligentesQA/Home/Index"
        },
        complete: function () { }
    });
}
function getFirmas()
{
    $.ajax({
        type: "GET",
        url: "/Home/getFirmas",
        //url: "/cajerosinteligentesQA/Home/getFirmas,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data);
            //console.log(data.clave_Prospecto);
            //console.log(data.nombre_Prospecto);
            for (var i = 0; i < data.length; i++) {
                if (data[i].rol === "ReprecentanteLegal") {
                    sessionStorage.setItem("ReprecentanteLegal",data[i].nombre)
                    sessionStorage.setItem("ReprecentanteLegalFirma",data[i].firma)
                }
                if (data[i].rol === "Testigo") {
                    sessionStorage.setItem("Testigo", data[i].nombre)
                    sessionStorage.setItem("TestigoFirma", data[i].firma)
                }
            }
           
           
        },
        error: function (error) {
        },
        complete: function () { }
    });
}

function SelectDocumentoActual() {
    if (sessionStorage.getItem("Documento") === "7") {
        sessionStorage.setItem("Documento", "0")
        $("#step-6").removeAttr("checked");
        $("#step-5").removeAttr("checked");
        $("#step-4").removeAttr("checked");
        $("#step-3").removeAttr("checked");
        $("#step-2").removeAttr("checked");
        $("#step-1").removeAttr("checked");
        $("#verDocumentos").addClass("visually-hidden")
        $("#guardarDocumentos").removeClass("visually-hidden")
        window.location.href = "/Home/addProspecto"
        //window.location.href = "/cajerosinteligentesQA/Home/addProspecto"
    }
    else if (sessionStorage.getItem("Documento") === "6") {
        $("#step-6").removeAttr("disabled");
        $("#step-6").attr("checked", "checked");
        $("#step-5").removeAttr("checked");
        $("#step-4").removeAttr("checked");
        $("#step-3").removeAttr("checked");
        $("#step-2").removeAttr("checked");
        $("#step-1").removeAttr("checked");
        window.location.href ="#doc6"
    }
    else if (sessionStorage.getItem("Documento") === "5") {
        $("#step-5").removeAttr("disabled");
        $("#step-5").attr("checked", "checked");
        $("#step-6").removeAttr("checked");
        $("#step-4").removeAttr("checked");
        $("#step-3").removeAttr("checked");
        $("#step-2").removeAttr("checked");
        $("#step-1").removeAttr("checked");
        window.location.href = "#doc5"
    }
    else if (sessionStorage.getItem("Documento") === "4") {
        $("#step-4").removeAttr("disabled");
        $("#step-4").attr("checked", "checked");
        $("#step-5").removeAttr("checked");
        $("#step-6").removeAttr("checked");
        $("#step-3").removeAttr("checked");
        $("#step-2").removeAttr("checked");
        $("#step-1").removeAttr("checked");
        window.location.href = "#doc4"
    } else if (sessionStorage.getItem("Documento") === "3") {
        $("#step-3").removeAttr("disabled");
        $("#step-3").attr("checked", "checked");
        $("#step-5").removeAttr("checked");
        $("#step-4").removeAttr("checked");
        $("#step-6").removeAttr("checked");
        $("#step-2").removeAttr("checked");
        $("#step-1").removeAttr("checked");
        window.location.href = "#doc3"
    } else if (sessionStorage.getItem("Documento") === "2") {
        $("#step-2").removeAttr("disabled");
        $("#step-2").attr("checked", "checked");
        $("#step-5").removeAttr("checked");
        $("#step-4").removeAttr("checked");
        $("#step-3").removeAttr("checked");
        $("#step-6").removeAttr("checked", "checked");
        $("#step-1").removeAttr("checked", "checked");
        window.location.href = "#doc2"
    } else if (sessionStorage.getItem("Documento") === "1") {
        $("#step-1").removeAttr("disabled");
        $("#step-1").attr("checked", "checked");
        $("#step-5").removeAttr("checked", "checked");
        $("#step-4").removeAttr("checked", "checked");
        $("#step-3").removeAttr("checked", "checked");
        $("#step-2").removeAttr("checked", "checked");
        $("#step-6").removeAttr("checked", "checked");
        window.location.href = "#doc1"
    } else if (sessionStorage.getItem("Documento")==="0") {
        $("#verDocumentos").addClass("visually-hidden")
        $("#guardarDocumentos").removeClass("visually-hidden")
        
    }
}

function DocumentosFirmados() {
    //Aviso Candidatos
    if (sessionStorage.getItem("Doc1_Firmado") === "Firmado") {
        $("#step-1").removeAttr("disabled");
        $("#firma_AvisoCandidato").attr('src', sessionStorage.getItem("Firma_Prospecto"));
        $("#firma_AvisoCandidato").removeClass("visually-hidden")
        $("#AvNombre_Prospecto").html(sessionStorage.getItem("Nombre_Prospecto"));
        $("#btn_Av_Candidato").addClass("visually-hidden")
    }

    //Carta Confidencialidad
    if (sessionStorage.getItem("Doc2_Firmado") === "Firmado") {
        $("#step-2").removeAttr("disabled");
        $("#firma_ContratoLaboral").attr('src', sessionStorage.getItem("Firma_Prospecto"));
        $("#firma_ContratoLaboral").removeClass("visually-hidden")
        $("#nombre_Contrato").html(sessionStorage.getItem("Nombre_Prospecto"));
        $("#nombre_Contrato2").html(sessionStorage.getItem("Nombre_Prospecto"));
        //sexo, edad, estadocivil, curp, rfc, domicilio, puesto
        $("#sexo").html(sessionStorage.getItem("Sexo_Prospecto"))
        $("#edad").html(sessionStorage.getItem("Edad_Prospecto"))
        $("#estadocivil").html(sessionStorage.getItem("EstadoCivil_Prospecto"))
        $("#curp").html(sessionStorage.getItem("Curp_Prospecto"))
        $("#rfc").html(sessionStorage.getItem("Rfc_Prospecto"))
        $("#domicilio").html(sessionStorage.getItem("Domicilio_Prospecto"))
        $("#puesto").html(sessionStorage.getItem("Puesto_Prospecto"))
        $("#btn-ContratoLaboral").addClass("visually-hidden")
    }
    //Convenio de Confidencialidad
    if (sessionStorage.getItem("Doc3_Firmado") === "Firmado") {
        $("#step-3").removeAttr("disabled");
        $("#firmaAvisoPrivacidad").attr('src', sessionStorage.getItem("Firma_Prospecto"));
        $("#firmaConvenioConfCandidato").removeClass("visually-hidden")
        $("#nombre_AvisoPrivacidad").html(sessionStorage.getItem("Nombre_Prospecto"));
        $("#btn_Av_Privacidad").addClass("visually-hidden")
    }
    //Aviso Privacidad
    if (sessionStorage.getItem("Doc4_Firmado") === "Firmado") {
        $("#step-4").removeAttr("disabled");
        $("#firmaCartaCandidato").attr('src', sessionStorage.getItem("Firma_Prospecto"));
        $("#firmaCartaCandidato").removeClass("visually-hidden")
        $("#nombre_Carta_Conf").html(sessionStorage.getItem("Nombre_Prospecto"));
        $("#btn_Carta_Confidencialidad").addClass("visually-hidden")
    }
    //Concentimientos
    if (sessionStorage.getItem("Doc5_Firmado") === "Firmado") {
        $("#step-5").removeAttr("disabled");
        $("#firmaConvenioConfCandidato").attr('src', sessionStorage.getItem("Firma_Prospecto"));
        $("#firmaConvenioConfCandidato").removeClass("visually-hidden")
        $("#nombre_Convenio_Conf").html(sessionStorage.getItem("Nombre_Prospecto"));
        $("#btn_Convenio_Conf").addClass("visually-hidden")
    }
    //Contrato Laboral
    if (sessionStorage.getItem("Doc6_Firmado") === "Firmado") {
        $("#step-6").removeAttr("disabled");
        $("#firmaConcentimiento").attr('src', sessionStorage.getItem("Firma_Prospecto"));
        $("#firmaConcentimiento").removeClass("visually-hidden")
        $("#nombre_Concentimientos").html(sessionStorage.getItem("Nombre_Prospecto"));
        $("#nombre_Concentimiento").html(sessionStorage.getItem("Nombre_Prospecto"));
        $("#btn_Concentimientos").addClass("visually-hidden")
    }
}