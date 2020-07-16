# Lenguaje S

Este programa nació para poder justificar no estudiar Lógica Computacional. Te permite compilar un programa en Lenguaje S a su numero de programa y también conseguir un programa a partir de su numero.

## Instalación

El programa esta hecho en C# con .NET Core. Esto significa que es multiplataforma. Si tienen instalados el runtime de .NET Core ([Link](https://dotnet.microsoft.com/download/dotnet/current)), pueden descargar la versión portable y correrla sin problemas. Si no tienen instalado el runtime y no lo quieren instalar, también esta la opción de descargar el ejecutable para cada plataforma junto al runtime.

## Uso

```powershell
# Para compilar simplemente hay que mandarle el path a un archivo donde este escrito el programa.

LenguajeS compile PathToFile

# Para decompilar solo hay que indicarle el codigo del programa

LenguajeS decompile CODE

# Opcionalmente se puede enviar el codigo decompilado a un archivo de texto. 
# Para hacerlo hay que agregar un flag

LenguajeS decompile CODE save PathToSave
```

## Sintaxis

Para poder generar el numero del programa a partir del codigo, hay que respetar la siguente sintaxis:

```powershell
# Las variables pueden estar en minuscula o mayuscula. Por ejemplo:
Y, y, X1, X123, Z999

# Las etiquetas toman valores de [A...E] y sus variaciones en minuscula tambien, por ejemplo:
A1, B1, C4, D4, E5

# Las instrucciones permitidas son las siguentes:

X1 <- X1
[A1]	X1 <- X1
A1 		X1 <- X1

Z2 <- Z2 + 1
[A1] 	Z2 <- Z2 + 1
A1 		Z2 <- Z2 + 1

Y <- Y - 1
[A1] 	Y <- Y - 1
A1 		Y <- Y - 1

IF X12 != 0 GOTO A1 
[A1]  	IF X12 != 0 GOTO A1 
A1 		IF X12 != 0 GOTO A1 

```



