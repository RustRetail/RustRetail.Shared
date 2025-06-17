using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedKernel.Domain.Extensions
{
    /// <summary>
    /// Provides extension methods for functional-style transformations and chaining on <see cref="Result"/> and <see cref="Result{T}"/> types.
    /// </summary>
    public static class ResultExtensions
    {
        #region Synchronous

        /// <summary>
        /// Maps the value of a successful <see cref="Result{T}"/> to a new value using the specified mapping function.
        /// </summary>
        /// <typeparam name="T">The type of the input value.</typeparam>
        /// <typeparam name="U">The type of the output value.</typeparam>
        /// <param name="result">The result to map.</param>
        /// <param name="mapper">The mapping function.</param>
        /// <returns>A new <see cref="Result{U}"/> containing the mapped value, or a failure result with the original errors.</returns>
        public static Result<U> Map<T, U>(this Result<T> result, Func<T, U> mapper)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            return result.IsSuccess
                ? Result.Success(mapper(result.Value))
                : Result.Failure<U>(result.Errors!);
        }

        /// <summary>
        /// Binds the value of a successful <see cref="Result{T}"/> to a new <see cref="Result{U}"/> using the specified binding function.
        /// </summary>
        /// <typeparam name="T">The type of the input value.</typeparam>
        /// <typeparam name="U">The type of the output value.</typeparam>
        /// <param name="result">The result to bind.</param>
        /// <param name="binder">The binding function.</param>
        /// <returns>The result of the binding function, or a failure result with the original errors.</returns>
        public static Result<U> Bind<T, U>(this Result<T> result, Func<T, Result<U>> binder)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (binder == null) throw new ArgumentNullException(nameof(binder));

            return result.IsSuccess
                ? binder(result.Value)
                : Result.Failure<U>(result.Errors!);
        }

        /// <summary>
        /// Binds a successful <see cref="Result"/> to a new <see cref="Result"/> using the specified binding function.
        /// </summary>
        /// <param name="result">The result to bind.</param>
        /// <param name="binder">The binding function.</param>
        /// <returns>The result of the binding function, or the original failure result.</returns>
        public static Result Bind(this Result result, Func<Result> binder)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (binder == null) throw new ArgumentNullException(nameof(binder));

            return result.IsSuccess
                ? binder()
                : result;
        }

        #endregion

        #region Asynchronous

        /// <summary>
        /// Asynchronously maps the value of a successful <see cref="Result{T}"/> to a new value using the specified mapping function.
        /// </summary>
        /// <typeparam name="T">The type of the input value.</typeparam>
        /// <typeparam name="U">The type of the output value.</typeparam>
        /// <param name="resultTask">The task producing the result to map.</param>
        /// <param name="mapper">The mapping function.</param>
        /// <returns>A task producing a new <see cref="Result{U}"/> containing the mapped value, or a failure result with the original errors.</returns>
        public static async Task<Result<U>> MapAsync<T, U>(this Task<Result<T>> resultTask, Func<T, U> mapper)
        {
            if (resultTask == null) throw new ArgumentNullException(nameof(resultTask));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? Result.Success(mapper(result.Value))
                : Result.Failure<U>(result.Errors!);
        }

        /// <summary>
        /// Asynchronously maps the value of a successful <see cref="Result{T}"/> to a new value using the specified asynchronous mapping function.
        /// </summary>
        /// <typeparam name="T">The type of the input value.</typeparam>
        /// <typeparam name="U">The type of the output value.</typeparam>
        /// <param name="result">The result to map.</param>
        /// <param name="asyncMapper">The asynchronous mapping function.</param>
        /// <returns>A task producing a new <see cref="Result{U}"/> containing the mapped value, or a failure result with the original errors.</returns>
        public static async Task<Result<U>> MapAsync<T, U>(this Result<T> result, Func<T, Task<U>> asyncMapper)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (asyncMapper == null) throw new ArgumentNullException(nameof(asyncMapper));

            if (!result.IsSuccess)
                return Result.Failure<U>(result.Errors!);

            var mappedValue = await asyncMapper(result.Value).ConfigureAwait(false);
            return Result.Success(mappedValue);
        }

        /// <summary>
        /// Asynchronously binds the value of a successful <see cref="Result{T}"/> to a new <see cref="Result{U}"/> using the specified asynchronous binding function.
        /// </summary>
        /// <typeparam name="T">The type of the input value.</typeparam>
        /// <typeparam name="U">The type of the output value.</typeparam>
        /// <param name="resultTask">The task producing the result to bind.</param>
        /// <param name="asyncBinder">The asynchronous binding function.</param>
        /// <returns>A task producing the result of the binding function, or a failure result with the original errors.</returns>
        public static async Task<Result<U>> BindAsync<T, U>(this Task<Result<T>> resultTask, Func<T, Task<Result<U>>> asyncBinder)
        {
            if (resultTask == null) throw new ArgumentNullException(nameof(resultTask));
            if (asyncBinder == null) throw new ArgumentNullException(nameof(asyncBinder));

            var result = await resultTask.ConfigureAwait(false);

            if (!result.IsSuccess)
                return Result.Failure<U>(result.Errors!);

            return await asyncBinder(result.Value).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously binds the value of a successful <see cref="Result{T}"/> to a new <see cref="Result{U}"/> using the specified asynchronous binding function.
        /// </summary>
        /// <typeparam name="T">The type of the input value.</typeparam>
        /// <typeparam name="U">The type of the output value.</typeparam>
        /// <param name="result">The result to bind.</param>
        /// <param name="asyncBinder">The asynchronous binding function.</param>
        /// <returns>A task producing the result of the binding function, or a failure result with the original errors.</returns>
        public static async Task<Result<U>> BindAsync<T, U>(this Result<T> result, Func<T, Task<Result<U>>> asyncBinder)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (asyncBinder == null) throw new ArgumentNullException(nameof(asyncBinder));

            if (!result.IsSuccess)
                return Result.Failure<U>(result.Errors!);

            return await asyncBinder(result.Value).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously binds a successful <see cref="Result"/> to a new <see cref="Result"/> using the specified asynchronous binding function.
        /// </summary>
        /// <param name="resultTask">The task producing the result to bind.</param>
        /// <param name="asyncBinder">The asynchronous binding function.</param>
        /// <returns>A task producing the result of the binding function, or the original failure result.</returns>
        public static async Task<Result> BindAsync(this Task<Result> resultTask, Func<Task<Result>> asyncBinder)
        {
            if (resultTask == null) throw new ArgumentNullException(nameof(resultTask));
            if (asyncBinder == null) throw new ArgumentNullException(nameof(asyncBinder));

            var result = await resultTask.ConfigureAwait(false);

            if (!result.IsSuccess)
                return result;

            return await asyncBinder().ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously binds a successful <see cref="Result"/> to a new <see cref="Result"/> using the specified asynchronous binding function.
        /// </summary>
        /// <param name="result">The result to bind.</param>
        /// <param name="asyncBinder">The asynchronous binding function.</param>
        /// <returns>A task producing the result of the binding function, or the original failure result.</returns>
        public static async Task<Result> BindAsync(this Result result, Func<Task<Result>> asyncBinder)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (asyncBinder == null) throw new ArgumentNullException(nameof(asyncBinder));

            if (!result.IsSuccess)
                return result;

            return await asyncBinder().ConfigureAwait(false);
        }

        #endregion
    }
}
