# Entrada 04/12/2024

- Autor: Stefano Vidaurre Olite

## Trabajo realizado

- Refactorizar GameControllerCollection
  - La responsabilidad de gestionar la relación controller/view es de  
  GameControllerViewMatcher
  - Se elimina la interfaz IGameControllerViewCollection
- Se añaden validaciones para comprobar que se registran correctamente los  
controladores y las vistas
- Se implementa la gestión de scopes
  - Los controllers y las views ya no son singleton
  - Cuando cambiamos de vista/controller liberamos memoria
- Se implementa un sistema de loggin usando ILogger como estándar
  - Por debajo usa el logger de Raylib

## Trabajo pendiente

- Los logs internos deben ser de nivel Debug
- Es necesario incluir la posibilidad de configurar el logger

## Notas

- Queda pendiente investigar Serilog + RaylibLogger
- Utilizar el patron observer para gestionar los cambios en los viewmodel
- Unificar las dos interfaces de vista si no es necesario el método Draw()
- Es posible que sea necesario recuperar el atributo GameViewAttribute
- Gestión de dibujar diferentes niveles de capas en la vista
- Crear Test para fijar funcionalidad
