--Общий вывод
Select Em.Name, D.Name, Et.TaskText, Et.LaborIntensity, Et.ImportantTask
FROM EmployeeTasks AS Et 
JOIN Employes AS Em ON Et.EmployeId = Em.Id
JOIN Department AS D ON Em.DepartmentId = D.Id

--Кол-во сотрудников в отделе
Select D.Id, COUNT(Em.Id) AS EmpCount
FROM Employes AS Em 
JOIN Department AS D ON Em.DepartmentId = D.Id
GROUP BY D.Id, Em.DepartmentId

--Кол-во задач на сотрудников отдела
Select D.Id, COUNT(Et.Id) AS TaskCount
FROM Employes AS Em 
JOIN Department AS D ON Em.DepartmentId = D.Id
JOIN EmployeeTasks AS Et ON Em.Id = Et.EmployeId
GROUP BY D.Id

--Трудоемкость важных задач отдела
Select D.Id, SUM(Et.LaborIntensity) AS SumLaborIntensityImportantTasks
FROM Employes AS Em 
JOIN Department AS D ON Em.DepartmentId = D.Id
JOIN EmployeeTasks AS Et ON Em.Id = Et.EmployeId
WHERE Et.ImportantTask = 'Y'
GROUP BY D.Id

--Общая трудоемкость задач отдела
Select D.Id, D.Name, SUM(Et.LaborIntensity) AS SumLaborIntensity
FROM Employes AS Em 
JOIN Department AS D ON Em.DepartmentId = D.Id
JOIN EmployeeTasks AS Et ON Em.Id = Et.EmployeId
GROUP BY D.Id, D.Name
