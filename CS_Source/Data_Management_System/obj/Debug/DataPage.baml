   M S B A M L   `   `   `  ����Q  MData_Management_System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Data_Management_SystemData_Management_System  5      %    Data_Management_System.DataPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:Data_Management_System     5        N� Title$  DataPage��5	      .��5
      � .+�5   
   �� 
�������$��1q�6%   $
��Left=�69   $	��Top=�6T   $��60, 0, 0, 100q�6l   ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5         AutoGenerateColumns .   �� AlternationCount$ 1��6E      GridLinesVisibility$ 
Horizontal=�5         HorizontalGridLinesBrush
 �����   CanUserAddRows .  $
��Left=�5      ��5   ,   �� *
Students+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 学号��6)   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 65   �� *Id+  5      $ 性别��6)   	 65   �� *Sex+  5      $ 姓名��6)   	 65   �� *Name+  5      $ 成绩��6)   	 65   �� *Grade+  5      $ 年龄��6)   	 65   �� *Age+4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Actions��6-   
   HeaderTemplate
 5      �� 5      .R�5   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5   &   �� $��编辑��60   $��Center=�6:   o� 5   &    o�Name$ 
sortButton��6+   -   5        .@�5   *   m� $��排序��63   .@�5    .   m� -   5        $��从高到低��5    7   6E   �� 5!   .   m� 5"   .   -   5        $��从低到高��5"   7   6E   �� 5#   .   5$   +   5%   '   5'   #   5(         CellTemplate 5*      �� 5+      .R�5,   "   �� $ 
Horizontal=�6-   .+�5-   &   �� -   5        $
��Edit��5-   -   ,   &Data_Management_System.GlobalVariables   
is_teacher#{�Z 6U   �� 5.   &   -   5        $��Delete��5.   -   #{�Z 6Y   5/   #   50      52      54      55      �� 56   
   $ 
Horizontal=�6   $��Center=�6.   $��Bottom=�6K   $��0, 0, 0, 20q�6f   .+�57      �� -   5        $	��Add��57      $��10q�6;   #{�Z 6G   �� 58      -   5        $��Search��58      $��10q�6A   59      5:      5;      