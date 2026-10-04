# Desafio Target Sistemas

Resolução do desafio técnico para a vaga de Desenvolvedor de Sistemas Jr.

Feito em **C# com .NET 8**, como aplicação de console, com testes em **xUnit**.

## Como executar

Pré-requisito: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
dotnet run --project src/DesafioTarget
```

Abre um menu para escolher o exercício:

```
===== Desafio Target Sistemas =====
1 - Comissão dos vendedores
2 - Movimentação de estoque
3 - Cálculo de juros
0 - Sair
```

Para rodar os testes:

```bash
dotnet test
```

## Estrutura

```
src/DesafioTarget/
├── Comissoes/       exercício 1
├── Estoque/         exercício 2
├── Juros/           exercício 3
├── Compartilhado/   leitura de JSON, entrada do console e formatação de moeda
└── Dados/           vendas.json e estoque.json do enunciado
tests/DesafioTarget.Tests/
```

Em cada exercício, a regra de negócio (`CalculadoraComissao`, `ControleEstoque`, `CalculadoraJuros`)
fica separada da parte de tela (`*Tela.cs`). Assim a lógica pode ser testada sem depender do console.

## 1. Comissão de vendas

Lê o `vendas.json` e calcula a comissão de cada venda pela regra:

| Valor da venda            | Comissão |
|---------------------------|----------|
| abaixo de R$ 100,00       | 0%       |
| de R$ 100,00 a R$ 499,99  | 1%       |
| a partir de R$ 500,00     | 5%       |

Depois agrupa por vendedor:

```
Vendedor              Vendas       Total vendido        Comissão
----------------------------------------------------------------
João Silva                10        R$ 10.754,70       R$ 495,68
Maria Souza                9         R$ 9.874,30       R$ 465,95
Carlos Oliveira            8         R$ 7.928,35       R$ 379,37
Ana Lima                   9         R$ 8.763,95       R$ 404,98
----------------------------------------------------------------
Total de comissões                                   R$ 1.745,98
```

## 2. Movimentação de estoque

Carrega os produtos do `estoque.json` e permite lançar entradas e saídas.
Cada movimentação tem:

- um **número identificador único** (sequencial);
- uma **descrição** informada pelo usuário (ex.: "Compra de fornecedor", "Venda", "Devolução");
- o tipo (entrada ou saída), a quantidade, a data e o saldo antes e depois.

Ao final de cada lançamento, o programa mostra a **quantidade final em estoque** do produto.

Validações:

- o produto precisa existir;
- a quantidade tem que ser maior que zero;
- a descrição é obrigatória;
- não é permitido dar saída maior do que o saldo disponível.

As movimentações ficam em memória enquanto o programa está aberto. O JSON original não é alterado.

## 3. Cálculo de juros

Recebe um valor e uma data de vencimento e calcula os juros até a data de hoje, com multa de
2,5% ao dia sobre o valor original (juros simples):

```
juros = valor × 0,025 × dias em atraso
```

Exemplo, com vencimento em 24/09/2026 e cálculo em 04/10/2026:

```
Valor original:   R$ 1.000,00
Dias em atraso:   10
Juros:            R$ 250,00
Valor atualizado: R$ 1.250,00
```

Se o vencimento for hoje ou uma data futura, não há juros.
O valor pode ser digitado como `1.500,00` ou `1500.00`.

## Decisões

- Valores em dinheiro usam `decimal`, para evitar os erros de arredondamento do `double`.
- A comissão é calculada venda a venda, como pede o enunciado. O arredondamento para centavos
  é feito só no total de cada vendedor.
- O cálculo de juros aceita a data de hoje como parâmetro, o que permite testar com datas fixas.
