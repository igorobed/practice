using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLab5Final
{
    public class BaseValidator<T>
    {
        protected BaseValidator() { } //чтобы не получилось создать объект в Main

        private List<Predicate<T>> predicates_list = new List<Predicate<T>>();

        public class Builder
        {
            private List<Predicate<T>> predicates_list = new List<Predicate<T>>();
            public Builder Add(Predicate<T> rule)
            {
                predicates_list.Add(rule);
                return this;
            }

            public BaseValidator<T> GetValidator()
            {
                return new BaseValidator<T>() { predicates_list = this.predicates_list };
            }
        }

        public static Builder CreateBuilder()
        {
            return new Builder();
        }

        public void Validate(T x)
        {
            if (predicates_list.Count == 0)
            {
                throw new EmptyRuleListException("The rule list is empty.");
            }
            int n = 1;
            foreach (Predicate<T> item in predicates_list)
            {
                if (item(x))
                {
                    Console.WriteLine(true);
                }
                else
                {
                    Console.WriteLine(false);
                    throw new RuleFalseException($"Тип валидируемого объекта: {(x.GetType()).ToString()}\nНомер проверки: {n}");
                }
                n += 1;
            }
        }
    }
}
