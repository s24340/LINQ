﻿using LinqTutorials.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqTutorials
{
    public static class LinqTasks
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        static LinqTasks()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts

            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;

            #endregion

            #region Load emps

            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion
        }

        public static IEnumerable<Emp> Task1()
        {
            return Emps.Where(emp => emp.Job == "Backend programmer");
        }

        public static IEnumerable<Emp> Task2()
        {
            return Emps.Where(emp => emp.Job == "Frontend programmer" && emp.Salary > 1000).OrderByDescending(emp => emp.Ename);
        }

        public static int Task3()
        {
            return Emps.Max(emp => emp.Salary);
        }

        public static IEnumerable<Emp> Task4()
        {
            int maxSalary = Emps.Max(emp => emp.Salary);
            return Emps.Where(emp => emp.Salary == maxSalary);
        }

        public static IEnumerable<object> Task5()
        {
            return Emps.Select(emp => new { Nazwisko = emp.Ename, Praca = emp.Job });
        }

        public static IEnumerable<object> Task6()
        {
            return Emps.Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new { emp.Ename, emp.Job, dept.Dname });
        }

        public static IEnumerable<object> Task7()
        {
            return Emps.GroupBy(emp => emp.Job).Select(group => new { Praca = group.Key, LiczbaPracownikow = group.Count() });
        }

        public static bool Task8()
        {
            return Emps.Any(emp => emp.Job == "Backend programmer");
        }

        public static Emp Task9()
        {
            return Emps.Where(emp => emp.Job == "Frontend programmer").OrderByDescending(emp => emp.HireDate).FirstOrDefault();
        }

        public static IEnumerable<object> Task10()
        {
            var unionResult = Emps.Select(emp => new { emp.Ename, emp.Job, emp.HireDate })
                                  .Union(new[] { new { Ename = "Brak wartości", Job = (string)null, HireDate = (DateTime?)null } });
            return unionResult;
        }

        public static IEnumerable<object> Task11()
        {
            return Emps.GroupBy(emp => emp.Deptno)
                       .Where(group => group.Count() > 1)
                       .Select(group => new { name = Depts.First(dept => dept.Deptno == group.Key).Dname, numOfEmployees = group.Count() });
        }

        public static IEnumerable<Emp> Task12()
        {
            return Emps.GetEmpsWithSubordinates();
        }

        public static int Task13(int[] arr)
        {
            return arr.GroupBy(x => x).Single(g => g.Count() % 2 != 0).Key;
        }

        public static IEnumerable<Dept> Task14()
        {
            return Depts.Where(dept => Emps.Count(emp => emp.Deptno == dept.Deptno) == 5 || !Emps.Any(emp => emp.Deptno == dept.Deptno))
                        .OrderBy(dept => dept.Dname);
        }

        public static IEnumerable<Dept> Task15()
        {
            return Emps.Where(emp => emp.Job.Contains("A"))
                       .GroupBy(emp => emp.Job)
                       .Where(group => group.Count() > 2)
                       .OrderByDescending(group => group.Count())
                       .Select(group => new { Praca = group.Key, LiczbaPracownikow = group.Count() });
        }

        public static IEnumerable<Dept> Task16()
        {
            return Emps.Select(emp => new { emp.Ename, emp.Job, emp.Deptno })
                       .Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new { emp.Ename, emp.Job, dept.Dname });
        }
    }

    public static class CustomExtensionMethods
    {
        public static IEnumerable<Emp> GetEmpsWithSubordinates(this IEnumerable<Emp> emps)
        {
            var result = emps.Where(e => emps.Any(e2 => e2.Mgr == e.Mgr)).OrderBy(e => e.Ename).ThenByDescending(e => e.Salary);
            return result;
        }
    }
}
