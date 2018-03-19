-- script on xolane side, 
-- controling aspects of the plane need for landing========

-- -- -- 8< -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
 dataref( "num", "sim/aircraft/view/acf_tailnum" )
dataref("xp_groundspeed", "sim/flightmodel/engine/ENGN_EGT")

dataref( "yPos","sim/flightmodel/position/local_y")

dataref( "zPos","sim/flightmodel/position/local_z")
dataref( "tailhook", "sim/flightmodel/controls/tailhook_ratio" )
dataref( "gears", "sim/aircraft/parts/acf_gear_deploy" )
dataref( "shipH", "sim/world/boat/carrier_deck_height_mtr" )

dataref( "wave", "sim/weather/wave_amplitude" )
--more datarefs

dataref( "puu", "sim/flightmodel/forces/M_total"  )

dataref( "py", "sim/world/boat/y_mtr" )
dataref( "pz", "sim/world/boat/z_mtr" )

dataref( "wbrak", "sim/cockpit2/controls/parking_brake_ratio" )

dataref( "px", "sim/flightmodel/failures/aoa_ice" , "writable")
dataref( "ice2", "sim/flightmodel/failures/aoa_ice2" , "writable")
dataref( "wy", "sim/flightmodel/forces/lift_path_axis" )

--set( "sim/operation/override/override_throttles",   1 )
set( "sim/cockpit2/controls/flap_ratio",   (.6) )

function draw_trim_info1()
  
  if wbrak > 0.4  then
  set( "sim/cockpit2/controls/flap_ratio",   (wbrak) )
  elseif wbrak < 0 then
  set( "sim/cockpit2/controls/flap_ratio",   0 )
  end
    qq=math.floor(puu+0.5)
    --deck height
    Height=yPos-(py+shipH)-2
    
  -- upward wind force on plance from carrier
    pzz=zPos-pz
    if (540 > pzz and pzz > 500) then
    set( "sim/flightmodel/forces/fnrml_plug_acf",   ( 150000) )
    end
  --down draft of carrier
    if (495 > pzz and pzz > 400) then
    set( "sim/flightmodel/forces/fnrml_plug_acf",   ( -100000) )
    end
  --tailhook and gears work together
    set( "sim/flightmodel/controls/tailhook_ratio",   gears )
    --dataref replacement
    set( "sim/flightmodel/failures/prop_ice",   pz )
    set( "sim/cockpit/radios/transponder_code",   Height )
    
    windDirection = (math.fmod(px , 1) * 1000)
    
    set( "sim/weather/wind_speed_kt[0]",   px )
    set( "sim/weather/wind_speed_kt[1]",   px )
    set( "sim/weather/wind_direction_degt[0]",   windDirection )

  
    --XPLMSetGraphicsState(0,0,0,1,1,0,0)
    --glColor4f(1, 1, 1, 0.5)
    --glRectf(300, 300, 310, 300)
    --glBegin_LINES()
    --x , y =  bubble(100, 250, wy , "force y")
    --x , y =  bubble(100, 390, qq , "pitch delta")
    --x , y =  bubble(100, 320, px , "windspeed(aoa)")
    --x , y =  bubble(100, 605, windDirection , "Direction wind")
    --x , y =  bubble(240, 250, num,  "tailnum")
    --x , y =  bubble(100, 450, xp_groundspeed*100 , "vel")
    --glEnd()
end

do_every_draw("draw_trim_info1()")


------------------------------------



 

