   M S B A M L   `   `   `  ����K  GGoogle Translate, Version=0.0.0.6, Culture=neutral, PublicKeyToken=null3clr-namespace:Google_TranslateGoogle_Translate  5   	   !    Google_Translate.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F BNHotkey.Wpf, Version=1.2.1.0, Culture=neutral, PublicKeyToken=nullF 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:Google_Translate     5   	   \ XHardcodet.Wpf.TaskbarNotification, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  5   	   -   5          � Title$  Google Translate��5   	   $	��120��6"   $	��551��60   $	��800��6<    � Topmost .   � WindowStyle$
 None=�5   G    � 
ResizeMode$ NoResize=�5   5    � 	Resources 5         
BlueButton �� 
TargetType �� Property �� Value  
GrayButton  ComboTrigger �� Triggers	 P� Property
 P� Value��%X  &	        &	 �     &	 �    �� 5   
   
BlueButton��" ��67   .�5	      ��  � �� �#4B8DF86+   �� 5
       � ��
 �����6+   �� 5       � ��$ 15(�6)   �� 5       � ��$ Segoe UI Symbol)�6+   5      �� 5   
   
GrayButton��" ��67   .�5      ��  � �� �#E3E3E36+   �� 5       � ��
 �   �6+   �� 5       � ��$ 15(�6)   �� 5       � ��$ Segoe UI Symbol)�6+   5      �� 5   
   " ��6%    5      P� 5      	 �@��
IsSelected
 . . �5      ��  � ��$ Center=�6C   5      5      5      .��5      � $��3q�6    � ColumnDefinitions 5   
   �� 5       $��* �6   �� 5!      $��* �6    � RowDefinitions 5#   
   �� 5$      $��40 �6   �� 5%      $
��Auto �6   �� 5&      $��35 �6   .+�5)   
   3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  -   5          Name$ myNotifyIcon��5)      $y�Visible=�5*         ToolTipText$ Google Translate��5+         
IconSource$ icon.ico��5,         MenuActivation$ 
RightClick=�5-         PopupActivation$ DoubleClick=�5.       � ContextMenu 5/      �� 50      .@�51      m� -   5        $��AutostartOption��51   F   $��	Autostart��6    m� IsCheckable . 6   m� 52      -   5        $��Ustaw skrót klawiszowy��52      6?   m� 53      -   5        $��Zamknij��53      60   54      58      �� 5:   
   $��Tłumacz��6   $��20(�6"   $��Segoe UI Symbol)�60   ���#DD4B396M   �� 5<   
   -   5        $��	PinBorder��5<      ���#C0C0C0 �� CornerRadius$ 5��68   $��1q�6I   $��1��6]   $��30��6m   $��70��6y   $��Right=�6�   $��0,0,75,0q�5=      ._�5>      �� -   5        $��BtnPin��5>      #��[ 6#   $
��📌��6\   5?      �� 5@   
   ���#4B8DF8$ 5��6'   $��1q�68   $��1��6L   $��70��6\   $��30��6g   $��Right=�6s   ._�5A      �� -   5        $��BtnHide��5A      #��[  6$   $	��❌��6H   $��Center=�6[   $��Center=�6   5B      5C      �� 5E   
   -	   5        $��Input��5E      $��1��6    $��0,5,3,0q�6-   
������ $	��710��6W    �� TextWrapping$
 Wrap=�6g   $��15(�6{   $
��Auto��6�   $
��Auto��6�   $��Segoe UI Symbol)�5F      $��5q�6G   $
��Auto=�6S   �� 5H   
   -
   5        $��Result��5H      $��1��6    $��1��60   
�������$	��710��6T   $��3,5,0,0q�6d   $��Segoe UI Symbol)�6u   $
��Auto��6�   $
��Auto��6�   $��15(�5I      $
 Wrap=�6"   $��5q�66   $
��Auto=�6B    �� 
IsReadOnly . 6r   �� 5K   
   ���#4B8DF8$ 5��6'   $��1q�68   $��1��6L   $��2��6\   $��0,5,0,0q�6i   $��Right=�6z   ._�5L      �� -   5        $
��Send��5L      $��Tłumacz��6!   $	��100��6F   #��[  6R   5M      �� 5O   
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��2��6;   $��0,5,0,0q�6O   .+�5Q      �� -   5        $��	SetSource��5Q      ���#E3E3E3$��0,0,5,0q�6k   $	��130��6|   $��Segoe UI Symbol)�6�   $��15(�5R      �� 5S      -   5        $��PlToEnBorder��5S   +   ���#E3E3E3$ 5��6?   $��1q�6P   $��0��6d   $��2��6t   $
��Left=�6�   $��0,0,5,0q�5T      ._�5U      �� -   5        $��PlToEn��5U      $	��⇄��6'   ���#E3E3E3$��70��6O   ���#666$��Segoe UI Symbol)�6l   $��30(�6�   $��	0,-10,0,0q�5V      6-   5W      �� 5X      -   5        $��	GetResult��5X      ���#E3E3E3$	��130��6k   $��Segoe UI Symbol)�6w   $��15(�6�   5Z      5]      5^      