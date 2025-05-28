using RustRetail.SharedKernel.Domain.Abstractions;

namespace RustRetail.SharedKernel.Domain.Extensions
{
    public static class ResultExtensions
    {
        #region Synchronous

        public static Result<U> Map<T, U>(this Result<T> result, Func<T, U> mapper)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            return result.IsSuccess
                ? Result.Success(mapper(result.Value))
                : Result.Failure<U>(result.Errors!);
        }

        public static Result<U> Bind<T, U>(this Result<T> result, Func<T, Result<U>> binder)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (binder == null) throw new ArgumentNullException(nameof(binder));

            return result.IsSuccess
                ? binder(result.Value)
                : Result.Failure<U>(result.Errors!);
        }

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

        public static async Task<Result<U>> MapAsync<T, U>(this Task<Result<T>> resultTask, Func<T, U> mapper)
        {
            if (resultTask == null) throw new ArgumentNullException(nameof(resultTask));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? Result.Success(mapper(result.Value))
                : Result.Failure<U>(result.Errors!);
        }

        public static async Task<Result<U>> MapAsync<T, U>(this Result<T> result, Func<T, Task<U>> asyncMapper)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (asyncMapper == null) throw new ArgumentNullException(nameof(asyncMapper));

            if (!result.IsSuccess)
                return Result.Failure<U>(result.Errors!);

            var mappedValue = await asyncMapper(result.Value).ConfigureAwait(false);
            return Result.Success(mappedValue);
        }

        public static async Task<Result<U>> BindAsync<T, U>(this Task<Result<T>> resultTask, Func<T, Task<Result<U>>> asyncBinder)
        {
            if (resultTask == null) throw new ArgumentNullException(nameof(resultTask));
            if (asyncBinder == null) throw new ArgumentNullException(nameof(asyncBinder));

            var result = await resultTask.ConfigureAwait(false);

            if (!result.IsSuccess)
                return Result.Failure<U>(result.Errors!);

            return await asyncBinder(result.Value).ConfigureAwait(false);
        }

        public static async Task<Result<U>> BindAsync<T, U>(this Result<T> result, Func<T, Task<Result<U>>> asyncBinder)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (asyncBinder == null) throw new ArgumentNullException(nameof(asyncBinder));

            if (!result.IsSuccess)
                return Result.Failure<U>(result.Errors!);

            return await asyncBinder(result.Value).ConfigureAwait(false);
        }

        public static async Task<Result> BindAsync(this Task<Result> resultTask, Func<Task<Result>> asyncBinder)
        {
            if (resultTask == null) throw new ArgumentNullException(nameof(resultTask));
            if (asyncBinder == null) throw new ArgumentNullException(nameof(asyncBinder));

            var result = await resultTask.ConfigureAwait(false);

            if (!result.IsSuccess)
                return result;

            return await asyncBinder().ConfigureAwait(false);
        }

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
