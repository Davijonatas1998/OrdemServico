function formatCurrency(input) {

    var value = input.value.replace(/\D/g, '');
    var formattedValue = (parseFloat(value) / 100).toFixed(2);
    input.value = formattedValue;
}

function formatPercentage(input) {
    var value = input.value.replace(/\D/g, '');
    input.value = value + '%';
}

function calculateTotal() {
    var moedaInput = document.getElementById('moeda');
    var descontoInput = document.getElementById('desconto');
    var valorTotalSpan = document.getElementById('valorTotal');

    var moedaValue = parseFloat(moedaInput.value.replace(',', '.'));
    var descontoValue = parseFloat(descontoInput.value) || 0;

    var valorComDesconto = moedaValue - (moedaValue * (descontoValue / 100));
    valorTotalSpan.textContent = valorComDesconto.toFixed(2);
}

var moedaInput = document.getElementById('moeda');
moedaInput.addEventListener('input', function () {
    formatCurrency(this);
    calculateTotal();
});

var descontoInput = document.getElementById('desconto');
descontoInput.addEventListener('input', function () {
    formatPercentage(this);
    calculateTotal();
});
calculateTotal();