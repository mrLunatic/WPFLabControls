   M S B A M L   `   `   `  ����F  BLabControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:LabControls;assembly=LabControlsLabControls  5   	   M ISystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089_<clr-namespace:System.Collections.ObjectModel;assembly=SystemSystem.Collections.ObjectModel 5   	    WpfApplication1    WpfApplication1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   >Lab.clr-namespace:LabControls;assembly=LabControls     5   	   Mcoll<clr-namespace:System.Collections.ObjectModel;assembly=System     5   	     � Title$  
MainWindow��5   	   $	��671��6   $	��694��6)    � 	Resources 5         ClassicGauge �� 
TargetType   LabControls.ControlView �� Property �� Value   LabControls.Tick   Size   Fill   LabControls.Ramp   Fill   	Thickness  	CoolGauge	   Type�� 5   
   %�  &	        &	 A    �� 5	      "  6)   .�5
      ��  �@ 	MajorTick 5        5      $	 1 8��6#   
 �  ��6.   5      �� 5       �@ 	MinorTick 5        5      $	 1 4��6#   
 �  ��6.   5      �� 5       �@ Ramp 5        5      
 �   �$ 1Q�60   5      5      �� 5      "  6&   .�5      ��  �@ 	MajorTick 5        5      $	 8 8��6#   
 � ���$	 Triangle=�6<   5      �� 5        �@ 	MinorTick 5!        5"      $	 4 4��6#   
 � ���$	 Ellipse=�6<   5$      �� 5&       �@ Ramp 5'        5(      
 �   �$ 0Q�60   5*      5,      5-      .��50      � 
 � ShowGridLines
 . .+�52   
      LabControls.Gauge  -   5        $��gauge��52      $��30q�6#   #��[ 61      NegativeFill
 � � �   PositiveFill
 �� ��   Scale 54         LabControls.Scale  55         ValueMin$ 0Q�6      ValueMax$
 1000Q�6)      	MajorStep$	 100Q�6:      	MinorStep$ 20Q�6J      FormatString$ F0��6Y      Ranges 58         LabControls.Range     Start$ 0Q�6   
   End$	 500Q�6&      Fill
 ����60     59      $	 500Q�6   $	 900Q�6(   
 � ���62      Pointer 5;         LabControls.Pointer  5<         Size$ 1 100��6      Type$ 	Rectangle=�6+   5>         LabControls.HSlider  5A   
   -   5        $��slider��5A      $	��130q�6'   #��[ 64   
 � � �
 �� �� 5C        5D      $ 0Q�6   $
 1000Q�6)   $	 100Q�6:   $ 20Q�6J   $ F0��6Y    5G        $ 0Q�6   $	 500Q�6&   
 ����60     5H      $	 500Q�6   $	 900Q�6(   
 � ���62    5J        5K      $ 1 100��6   $ 	Rectangle=�6+   5M      5P      5Q      