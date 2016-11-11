$.validator.addMethod("customvalidationcpf", function (value, element, param) {
    return validaCPF(value); //chama um método validaCPF implementado em javascript
});
$.validator.unobtrusive.adapters.addBool("customvalidationcpf");

$.validator.addMethod("customvalidationcnpj", function (value, element, param) {
    return validaCNPJ(value); //chama um método validaCPF implementado em javascript
});
$.validator.unobtrusive.adapters.addBool("customvalidationcnpj");

function validaCPF(s) {

    if (s != undefined && s != null && s != "") {
        var i;
        var l = '';
        for (i = 0; i < s.length; i++) if (!isNaN(s.charAt(i))) l += s.charAt(i);
        s = l;
        if (s.length != 11) return false;
        var c = s.substr(0, 9);
        var dv = s.substr(9, 2);
        var d1 = 0;
        for (i = 0; i < 9; i++) d1 += c.charAt(i) * (10 - i);
        if (d1 == 0) return false;
        d1 = 11 - (d1 % 11);
        if (d1 > 9) d1 = 0;
        if (dv.charAt(0) != d1) return false;
        d1 *= 2;
        for (i = 0; i < 9; i++) d1 += c.charAt(i) * (11 - i)
        d1 = 11 - (d1 % 11);
        if (d1 > 9) d1 = 0;
        if (dv.charAt(1) != d1) return false;
    }
    return true;
}

function validaCNPJ(cnpj) {

    if (cnpj != undefined && cnpj != null && cnpj != "") {
        cnpj = cnpj.replace(/[^\d]+/g, '');

        if (cnpj == '') return false;

        if (cnpj.length != 14)
            return false;

        // Elimina CNPJs invalidos conhecidos
        if (cnpj == "00000000000000" ||
            cnpj == "11111111111111" ||
            cnpj == "22222222222222" ||
            cnpj == "33333333333333" ||
            cnpj == "44444444444444" ||
            cnpj == "55555555555555" ||
            cnpj == "66666666666666" ||
            cnpj == "77777777777777" ||
            cnpj == "88888888888888" ||
            cnpj == "99999999999999")
            return false;

        // Valida DVs
        tamanho = cnpj.length - 2
        numeros = cnpj.substring(0, tamanho);
        digitos = cnpj.substring(tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;

        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;
    }
    return true;

}