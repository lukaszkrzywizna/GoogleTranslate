   M S B A M L   `   `   `  ���� K  GGoogle Translate, Version=0.0.0.6, Culture=neutral, PublicKeyToken=null3clr-namespace:Google_TranslateGoogle_Translate  !    Google_Translate.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F BNHotkey.Wpf, Version=1.2.1.0, Culture=neutral, PublicKeyToken=nullF 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     0localclr-namespace:Google_Translate     \ XHardcodet.Wpf.TaskbarNotification, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  -     � Title$  Google Translate��$	��120��$	��551��$	��800�� � Topmost .   � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � 	Resources    
BlueButton �� 
TargetType �� Property �� Value  
GrayButton  ComboTrigger �� Triggers	 P� Property
 P� Value��%z  &	        &	 �     &	     �� 
BlueButton��" ��.���  � �� �#4B8DF8��  � ��
 �������  � ��$ 15(���  � ��$ Segoe UI Symbol)��� 
GrayButton��" ��.���  � �� �#E3E3E3��  � ��
 �   ���  � ��$ 15(���  � ��$ Segoe UI Symbol)��� " �� P� 	 �@��
IsSelected
 . . ���  � ��$ Center=�.��� $��3q� � ColumnDefinitions �� $��* ��� $��* � � RowDefinitions �� $��40 ��� $
��Auto ��� $��35 �.+�3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  -     Name$ myNotifyIcon��$y�Visible=�   ToolTipText$ Google Translate��   
IconSource$ icon.ico��   MenuActivation$ 
RightClick=�   PopupActivation$ DoubleClick=� � ContextMenu �� .@�m� -   $��AutostartOption��$��	Autostart�� m� IsCheckable . m� -   $��Ustaw skrót klawiszowy��m� -   $��Zamknij���� $��Tłumacz��$��20(�$��Segoe UI Symbol)����#DD4B39�� -   $��	PinBorder�����#C0C0C0 �� CornerRadius$ 5��$��1q�$��1��$��30��$��70��$��Right=�$��0,0,75,0q�._��� -   $��BtnPin��#��[ $
��📌���� ���#4B8DF8$ 5��$��1q�$��1��$��70��$��30��$��Right=�._��� -   $��BtnHide��#��[  $	��❌��$��Center=�$��Center=��� -	   $��Input��$��1��$��0,5,3,0q�
������ $	��710�� �� TextWrapping$
 Wrap=�$��15(�$
��Auto��$
��Auto��$��Segoe UI Symbol)�$��5q�$
��Auto=��� -
   $��Result��$��1��$��1��
�������$	��710��$��3,5,0,0q�$��Segoe UI Symbol)�$
��Auto��$
��Auto��$��15(�$
 Wrap=�$��5q�$
��Auto=� �� 
IsReadOnly . �� ���#4B8DF8$ 5��$��1q�$��1��$��2��$��0,5,0,0q�$��Right=�._��� -   $
��Send��$��Tłumacz��$	��100��#��[  ��  �� Orientation$ 
Horizontal=�$��2��$��2��$��0,5,0,0q�.+��� -   $��	SetSource�����#E3E3E3$��0,0,5,0q�$	��130��$��Segoe UI Symbol)�$��15(��� -   $��PlToEnBorder�����#E3E3E3$ 5��$��1q�$��0��$��2��$
��Left=�$��0,0,5,0q�._��� -   $��PlToEn��$	��⇄�����#E3E3E3$��70�����#666$��Segoe UI Symbol)�$��30(�$��	0,-10,0,0q��� -   $��	GetResult�����#E3E3E3$	��130��$��Segoe UI Symbol)�$��15(�