   M S B A M L   `   `   `  ����K  GGoogle Translate, Version=0.0.0.6, Culture=neutral, PublicKeyToken=null3clr-namespace:Google_TranslateGoogle_Translate  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   M ISystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089K2clr-namespace:System.Windows.Input;assembly=SystemSystem.Windows.Input 5   	   %    Google_Translate.ChangeShortcut   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F BNHotkey.Wpf, Version=1.2.1.0, Culture=neutral, PublicKeyToken=nullF 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Google_Translate     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	   DInput2clr-namespace:System.Windows.Input;assembly=System     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5
   	    � Title$ Zmień skrót��5   	   $	��160��6   $	��500��6*    � Icon$ icon.ico��6M    � 	Resources 5         
BlueButton �� 
TargetType �� Property �� Value��%�   &	        �� 5   
   
BlueButton��" ��67   .�5      ��  � �� �#4B8DF86+   �� 5       � ��
 �����6+   �� 5       � ��$ 15(�6)   �� 5       � ��$ Segoe UI Symbol)�6+   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��50��6(   ��6   Z� ._�5      �� $x��rPodaj skrót klawiszowy jakim chcesz pobierać i tłumaczyć tekst w programie. (Puste pole oznacza brak klawisza)��6   $
��Wrap=�6�   $��Segoe UI Symbol)�5      $��Center=�6-   $��Center=�6H    �� Padding$ 10q�6e   5      �� 5   
   	 �� Orientation$	 
Horizontal=�6   $��1��6.   $
��50,0q�6;   $��30��6I   $	��317��6U   .+�5       �� -   5        $��FirstKey��5       
 �� Text$

 Ctrl��6'   $��85��63   $��15(�6>   �� 5!      $��+��6   $��Center=�6    $��15(�6B   �� 5"      -   5        $��	SecondKey��5"      $	
 Alt��6(   $��85��63   $��15(�6>   �� 5#      $��+��6   $��Center=�6    $��15(�6B   �� 5$      -   5        $��ThirdKey��5$      $	��105��6'   $��15(�63   5%      �� 5'   
   -   5        $��2��5'      #��[  6   $��OK��6B   $	��120��6O   $��Center=�6[   $��0,5,0,5q�6x   $��15(�5(      5)      5*      