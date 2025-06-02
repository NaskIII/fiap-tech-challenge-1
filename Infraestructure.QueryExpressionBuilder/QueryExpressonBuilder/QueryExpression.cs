using Infraestructure.QueryExpressionBuilder.Messages;
using LinqKit;
using System.Linq.Expressions;

namespace Infraestructure.QueryExpressionBuilder.QueryExpresson
{
    public class QueryExpression<T> where T : class
    {

        private bool QueryInitialized { get; set; } = false;

        private Expression<Func<T, bool>>? query;

        public Expression<Func<T, bool>> Query
        {
            get
            {
                if (query == null)
                {
                    throw new InvalidOperationException(QueryBuilderMessages.QueryBuilderNotInitialized);
                }

                return query;
            }
            set
            {
                query = value ?? throw new InvalidOperationException(QueryBuilderMessages.QueryBuilderNotInitialized);
            }
        }

        public QueryExpression() { }

        public Expression<Func<T, bool>> True()
        {
            this.QueryInitialized = true;

            return PredicateBuilder.New<T>(true);
        }

        public Expression<Func<T, bool>> False()
        {
            this.QueryInitialized = true;

            return PredicateBuilder.New<T>(false);
        }

        internal QueryExpression<T> InitializeQueryChecker()
        {
            this.QueryInitialized = true;

            return this;
        }

        public QueryExpression<T> BeginFilter()
        {
            this.QueryInitialized = true;

            this.Query = this.True();

            return this;
        }

        public QueryExpression<T> Where(Expression<Func<T, bool>> expression)
        {
            if (this.QueryInitialized == true)
            {
                And(expression);

                return this;
            }

            Query = expression ?? throw new ArgumentNullException(nameof(expression).ToString(), QueryBuilderMessages.ExpressionIsNull);

            this.QueryInitialized = true;

            return this;
        }

        public QueryExpression<T> And(QueryExpression<T> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression).ToString(), QueryBuilderMessages.ExpressionIsNull);
            }
            else if (this.QueryInitialized == false)
            {
                throw new InvalidOperationException(QueryBuilderMessages.QueryIsNotInitialized);
            }

            Query = Query.And(expression.Build());

            return this;
        }

        public QueryExpression<T> And(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression).ToString(), QueryBuilderMessages.ExpressionIsNull);
            }
            else if (this.QueryInitialized == false)
            {
                throw new InvalidOperationException(QueryBuilderMessages.QueryIsNotInitialized);
            }

            Query = Query.And(expression);

            return this;
        }

        public QueryExpression<T> Or(QueryExpression<T> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression).ToString(), QueryBuilderMessages.ExpressionIsNull);
            }
            else if (this.QueryInitialized == false)
            {
                throw new InvalidOperationException(QueryBuilderMessages.QueryIsNotInitialized);
            }

            Query = Query.Or(expression.Build());

            return this;
        }

        public QueryExpression<T> Or(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression).ToString(), QueryBuilderMessages.ExpressionIsNull);
            }
            else if (this.QueryInitialized == false)
            {
                throw new InvalidOperationException(QueryBuilderMessages.QueryIsNotInitialized);
            }

            Query = Query.Or(expression);

            return this;
        }

        public QueryExpression<T> NestOr(QueryExpression<T> expression)
        {
            QueryExpression<T> innerQuery = new();
            innerQuery = innerQuery.Where(expression.Build());

            this.Or(innerQuery);

            return this;
        }

        public QueryExpression<T> NestOr(Expression<Func<T, bool>> expression)
        {
            QueryExpression<T> innerQuery = new();
            innerQuery = innerQuery.Where(expression);

            this.Or(innerQuery);

            return this;
        }

        public QueryExpression<T> NestAnd(Expression<Func<T, bool>> expression)
        {
            QueryExpression<T> innerQuery = new();
            innerQuery = innerQuery.Where(expression);

            this.And(innerQuery);

            return this;
        }

        public QueryExpression<T> NestAnd(QueryExpression<T> expression)
        {
            QueryExpression<T> innerQuery = new();
            innerQuery = innerQuery.Where(expression.Build());

            this.And(innerQuery);

            return this;
        }

        public Expression<Func<T, bool>> Build()
        {
            if (Query == null)
            {
                throw new InvalidOperationException(QueryBuilderMessages.QueryBuilderNotInitialized);
            }
            else if (this.QueryInitialized == false)
            {
                throw new InvalidOperationException(QueryBuilderMessages.QueryIsNotInitialized);
            }

            return Query;
        }

        public Func<T, bool> Compile()
        {
            if (Query == null)
            {
                throw new InvalidOperationException(QueryBuilderMessages.QueryBuilderNotInitialized);
            }
            else if (this.QueryInitialized == false)
            {
                throw new InvalidOperationException(QueryBuilderMessages.QueryIsNotInitialized);
            }

            return this.Build().Compile();
        }
    }
}
