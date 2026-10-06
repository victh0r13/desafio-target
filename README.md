Desafio Target Sistemas
Solução do desafio técnico em C# / .NET 8.

# Exercício O que faz

1 Comissão Calcula a comissão de cada vendedor a partir do vendas.json
2 Estoque Lança entradas e saídas de produtos e mostra o saldo final
3 Juros Calcula os juros de 2,5% ao dia sobre um valor vencido
Como rodar
Pré-requisito: .NET 8 SDK

dotnet run --project src/DesafioTarget
Vai abrir um menu no terminal, é só escolher o exercício.

No Visual Studio: abra o DesafioTarget.sln e aperte F5.

Testes
dotnet test
São 28 testes cobrindo as faixas de comissão, as validações do estoque e o cálculo de juros.

Estrutura
src/DesafioTarget/
├── Comissoes/
├── Estoque/
├── Juros/
├── Compartilhado/ leitura do JSON, entrada do teclado e formatação de moeda
└── Dados/ JSONs do enunciado
tests/DesafioTarget.Tests/
Observações
Valores em dinheiro usam decimal para evitar erro de arredondamento.
A comissão é calculada venda por venda e depois somada por vendedor.
As movimentações de estoque ficam em memória enquanto o programa está aberto.
Juros simples: valor × 2,5% × dias de atraso. Se não venceu, não há juros.
