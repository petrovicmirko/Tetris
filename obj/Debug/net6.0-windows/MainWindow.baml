   M S B A M L   `   `   `  ����A  =Tetris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TetrisTetris  5   	       Tetris.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Tetris     5   	   -   5          � Title$  Tetris��5   	   $	��400��6   $	��600��6%   $	��600��5	   	   $	��600��6    � 
ResizeMode$ CanMinimize=�5
   	   
�������$��RubikBrokenFax-Regular)�5   	   $��28(�6-   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   ��5   
   �� 5       �� ImageSource$! Assets/backgroundTetris.jpg��6   .+�5   
   (� $��1��6   $��0��5      $��0,0,0,20q�5      .��5       �� -   5         ��Name$ 
GameCanvas��5       ���#101010$	��250��5"      $	��510��5#      $
}�True��5$      5%      5&      �� 5(   
   -   5         ��Name$ 	ScoreText��5(      $��0��5)      $��0��5*      $��Score: ��5+      $��0, 10q�5,       �� TextAlignment$ Center=�5-      �� 5/   
   $��1��6   $��1��50      $	��Top=�51      $
��Left=�52      .+�53      �� $��Next:��6   $ Center=�54      �� 56      -   5         ��Name$ 	NextImage��56      $��20q�57      $	��125��58      59      �� 5;   
   $��1��6   $��1��5<      $��Bottom=�5=      $��Center=�5>      .+�5?      �� $��Highest score: ��6   $ Center=�5@      $��0,0,0,0q�5A      �� 5B      -   5        	 ��Name$	 textBoxHighScore��5B      
 �� 
IsReadOnly$

 True��5C       J� IsHitTestVisible$ False��5D      
������ �� TextAlignment$ Center=�5F      $��20q�5G      5H      � 5J   
   -   5         �Name$ GameOverMenu��5J      ���	#CC000000$��2��5L      $��3��5M      $y�Hidden=�5N      .+�5O      �� $��Center=�6   $��Center=�5P      .+�5Q      �� -   5        $ NewHighScore��5Q      $y�Hidden=�5R      $��48(�5S      $��SemiBold"�5T      $ Center=�5U      $��50q�5V      �� 5X      $��	Game Over��6   $��48(�5Y      $ Center=�5Z      �� 5\      -   5        $ FinalScoreText��5\      $��36(�5]      $ Center=�5^      �� 5`      -	   5        $ HighScoreText��5`      $��36(�5a      $ Center=�5b      �� 5d      -
   5        $��
Play again��5d      
������$��
30,20,30,0q�5f      $��5q�5g      5h      5i      5j      5k      5l      