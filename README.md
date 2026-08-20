# Projeto final de faculdade
Apesar da qualidade estar longe dos meus critérios de aceite em decorrência das inúmeras adversidades que surgiram durante a trajetória, o projeto foi o suficiente funcionando como um sistema simples de agendamento para uma barbearia/salão durante 3 meses.

## Quanto tempo levou para construir?
Literalmente, da ideia até o que está disponível aqui, tudo foi em 6 meses.
Apesar da branch "Main" ainda ser "Default", o verdadeiro projeto final é a branch "Raasch_Notebook".

## Dificuldades:
Primeiro que o projeto deveria ter sido um trabalho em grupo, mas infelizmente somente contribui com o desenvolvimento do código.
Combinando isso, com outras 5 disciplinas de 80 Horas Aulas da Faculdade + Estágio (praticamente trabalhando como Full Stack), não existia tempo para fazer o programa.
A partir de uma adaptação do software que trabalhava no Estágio, eu consegui modelar as primeiras páginas modelos que serviriam de referência para o resto.
O problema é que utilizei uma versão muito antiga do ASP.NET Core 2.1 / ASP.NET 3.5, por causa disso tive inúmeros problemas de incompatibilidade de código legado.
Eu até consegui contornar e instalar essas dependências numa máquina Ubuntu Headless, configurar o Nginx, alugar um domínio e realizar port forwarding.
Mas daí descobri que o software tinha incompatibilidades no redirecionamento HTTPS por causa da versão muito antiga e meu ISP não permitia alterar as rotas padrões.
Tive que improvisar um certificado de segurança próprio para uma comunicação segura.
Com esses problemas técnicos, e com a falta de tempo, não tive tempo para dedicar no visual do software.

## Aprendizado:
Definitivamente agora colocaria em práticas os conceitos de QA desde o início, dando preferência para o Left Shift Testing e antecipando problemas antes de tentar fazer o software inteiro e por último implantar.
A pressa foi minha inimiga mas agora estou com tempo para dedicar e avaliar todas as possibilidades.
