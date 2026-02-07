# Taller de Lenguajes II - Parcial de Práctica Integrador
**Tema:** Web API, Persistencia JSON, Herencia y Polimorfismo.

## 1. Escenario: Constructora "El Ladrillo S.A."
Una importante empresa constructora necesita migrar su gestión de alquileres a un sistema web. Actualmente, tienen problemas para calcular los costos operativos diarios de sus obras debido a la diversidad de equipos que utilizan.

Se solicita desarrollar una **Web API** que permita gestionar Obras y asignarles Equipos, calculando automáticamente los costos de alquiler basándose en reglas de negocio específicas.

## 2. Requerimientos Técnicos
El sistema debe respetar la arquitectura de **Separación de Responsabilidades** (similar al TP 5):
* **Modelos:** Clases de dominio anémicas o con lógica de negocio interna (sin dependencias de UI ni Acceso a Datos).
* **Acceso a Datos:** Clases encargadas exclusivamente de la persistencia en archivos **JSON**.
* **Controladores:** Endpoints de la API (ASP.NET Core) que gestionan las peticiones HTTP.

> **Restricción:** Está estrictamente prohibido el uso de `Console.WriteLine` o interacción por consola en cualquier clase del dominio o acceso a datos.

## 3. Reglas de Negocio (Dominio)

### A. Jerarquía de Equipos (Herencia y Polimorfismo)
Todo equipo tiene un `Nombre`, un `NumeroDeSerie` y un `CostoBase`. Sin embargo, el costo final de alquiler diario varía según el tipo:

1.  **Maquinaria Pesada (Grúas, Excavadoras, Aplanadoras):**
    * Tienen un atributo extra: `PesoToneladas`.
    * **Cálculo de Costo:** `CostoBase + (PesoToneladas * 150)`.
2.  **Herramientas de Mano (Taladros, Sierras, Martillos):**
    * Tienen un atributo extra: `Tipo` (Eléctrica o Manual) y `AniosDeUso`.
    * **Cálculo de Costo:** Es el `CostoBase`. **Excepción:** Si la herramienta es *Eléctrica* y tiene *más de 5 años de uso*, se aplica un **10% de descuento** sobre el precio base por obsolescencia.

### B. Validaciones Estrictas
El sistema debe impedir la carga de datos inválidos (Validar en Controller o Setter y devolver `400 Bad Request` si falla):
1.  **Número de Serie:** Debe ser una cadena de **exactamente 10 caracteres** (letras o números).
2.  **Obras:** No se pueden registrar obras con una fecha de inicio anterior al **01/01/2024**.

## 4. Funcionalidades de la API (Endpoints)

Debe implementar un controlador `ObrasController` con las siguientes operaciones:

1.  **[POST] Crear Obra:**
    * Recibe los datos de una nueva obra y la guarda en `Obras.json`.
    * Debe validar la fecha de inicio.

2.  **[PUT] Asignar Equipo a Obra:**
    * Recibe un `IdObra` y un objeto `Equipo` (puede ser Maquinaria o Herramienta).
    * Agrega el equipo a la lista de equipos de la obra y actualiza la persistencia.
    * Debe validar el `NumeroDeSerie` del equipo.

3.  **[GET] Obtener Informe de Obras:**
    * Devuelve un listado de todas las obras activas.
    * Cada objeto Obra en la respuesta debe incluir un campo calculado `CostoTotalDiario`, que es la suma de los costos de alquiler de todos sus equipos asignados (aplicando la lógica polimórfica).

## 5. Persistencia
* Implementar una clase `AccesoADatos` (o separada por entidad) que gestione la lectura y escritura de:
    * `obras.json`
* El manejo de archivos debe realizarse utilizando `System.Text.Json`.

---
**Criterios de Evaluación:**
* Correcta aplicación de Herencia y Polimorfismo para evitar `if/else` anidados en el cálculo de costos.
* Correcta implementación de los verbos HTTP (POST, PUT, GET).
* Manejo de Excepciones y códigos de estado (200 OK, 400 Bad Request).
* Limpieza del código y nomenclatura.