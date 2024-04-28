```mermaid
---
title: Diagrama Entidad Relacion
---
erDiagram
   Teacher ||..|{ Paises : r
   Teacher ||..|{ Universidades : r
   Teacher ||--|{ Grados : r
   Paises ||--|{ Universidades : r
```
