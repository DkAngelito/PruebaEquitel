# PruebaEquitel

El proyecto fue desarrollado teniendo en cuenta los principios SOLID. Para la parte de Inversion de Control se utiliza el patron 
de Inyeccion de dependencias con la libreria Simple Inyector, la cual es de las más sencillas y rápidas que conozco, pero es 
fácilmente reemplazable por cualquier otra.

El proyecto consta en tres partes. La primera parte es del cliente que contiene un servicio generador de textos aleatorios 
a partir de un alfabeto y un cliente de consola que consume este servicio y llama al servicio de analisis de textos a partir
de un cliente del servicio REST. Este cliente solicita el numero de textos a generar y el tamaño minimo y maximo (en caracteres)  
que pueden tener los textos. Luego de generados los envia al servicio.

La segunda parte es para el backend y consiste en el servicio analizador de textos y el sitio ASP.NET que lo hostea utilizando Web Api. 
El servicio utiliza un repositorio para escribir el texto y los datos del analisis. Las rutas que usa este repositorio para escribir
los archivos se configura en el web.config del sitio. La parte de escritura a disco era la parte mas demorada en el backend, por lo 
cual esta parte se realiza en una tarea separada. El portal, esta configurado actualmente para ser desplegado sobre IIS, y para 
mejorar el desempeño se pueden modificar algunos paramatetros del machine.config de la(s) máquina donde se haga el despliegue como:
los atributos del processModel como maxWorkerThreads, maxIoThreads, etc.

La tercera parte son unas entidades e interfaces transversales que se utilizan tanto en cliente como en backend.
