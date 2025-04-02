using MethodOfPotentials_C_;

CellPotentials cellPotentials = new CellPotentials();
Console.Write("Введите количество поставщиков: ");
int n = Convert.ToInt32(Console.ReadLine());
Console.Write("Введите количество заказчиков: ");
int m = Convert.ToInt32(Console.ReadLine());
cellPotentials.CellPot(n, m);
