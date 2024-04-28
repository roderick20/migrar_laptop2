## Sistema de Autenticación

::: mermaid
classDiagram

    class Auth {
    +login()
    +logout()
    +register-gp()
    +confirmaremail()
    +recoverypassword-gp()
    }

    class Home {
    +index()
    +changepassword-gp()
    }

:::

gp => get y post, por defecto get