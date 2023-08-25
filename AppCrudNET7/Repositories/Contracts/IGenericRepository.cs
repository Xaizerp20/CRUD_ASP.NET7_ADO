namespace AppCrudNET7.Repositories.Contracts
{


    // INTERFAZ IGenericRepository<T>
    // Esta interfaz define un conjunto genérico de métodos que permiten interactuar con diferentes tipos de datos representados por la clase T.
    // Los métodos reflejan operaciones comunes de acceso a datos, como listar, guardar, editar y eliminar registros.
    // Al usar esta interfaz, se promueve la reutilización de código y se abstrae la interacción con la capa de acceso a datos.
    // La restricción 'where T : class' garantiza que solo se pueden utilizar tipos de datos que sean clases.
    // Todos los métodos están diseñados para trabajar de manera asincrónica, lo que mejora la capacidad de respuesta de la aplicación.
    // Esta interfaz es fundamental en la arquitectura de acceso a datos, proporcionando una abstracción genérica para las operaciones CRUD.


    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> List();
        Task<bool> Save(T model);
        Task<bool> Edit(T model);
        Task<bool> Delete(T model);
    }
}
