O projeto Bookstore é uma aplicação web para gerenciar uma livraria online. 

Clean arquitecture foi a arquitetura utilizada: 

A Clean Architecture é um padrão de design de software que enfatiza a separação de preocupações e a inversão de dependências. Aqui estão os principais pontos:

![image](https://github.com/user-attachments/assets/0aa1cfdc-7078-42ab-bdb3-3eb1d163b20b)


- Separação de Camadas: A arquitetura é dividida em camadas bem definidas, com foco na lógica de negócios. Essas camadas incluem:
- Entidades: Representam os conceitos centrais do domínio.
-  Casos de Uso (Use Cases): Definem as regras de negócios específicas da aplicação.
- Adaptadores: Conectam as camadas externas (UI, bancos de dados) às internas.  
- Inversão de Dependências: A lógica de negócios não depende de detalhes de infraestrutura. Em vez disso, a infraestrutura depende do núcleo da aplicação.
- Testabilidade e Manutenibilidade: A separação de camadas facilita testes e modificações.
- Priorização do Núcleo da Aplicação: O foco está na lógica de negócios, mantendo-a isolada de preocup
-
- ações externas.

Também foi utilizado Mediator com Mediatr para intermediar a comunicação entre application, Api e Mvc.

Organização modular e estrutura de pastas:
![image](https://github.com/user-attachments/assets/20f6f848-e45c-4bcd-b5ef-03ec05599421)

Layout Clean:

![image](https://github.com/user-attachments/assets/6e097ac8-819a-414d-a6db-be1203591823)

![image](https://github.com/user-attachments/assets/5d1e066e-d14a-43c0-9a38-bad1b152319e)


