-- compensation for gear forces

-- works for armed f-18 fighter jet

dataref( "gears", "sim/aircraft/parts/acf_gear_deploy" )
--more datarefs

dataref( "num", "sim/aircraft/view/acf_tailnum" )

 dataref( "zPos","sim/flightmodel/position/local_z")
 dataref( "yy", "sim/flightmodel/position/local_y" )
 dataref( "zz", "sim/flightmodel/position/local_z" )
 
  dataref( "bb", "sim/flightmodel/engine/ENGN_EGT" )

 dataref( "zboat", "sim/world/boat/z_mtr" )
 dataref( "xboat", "sim/world/boat/x_mtr" )
 
 -- set grnd speed.
 
 set(  "sim/flightmodel/position/local_vz",    -90   )
 set(  "sim/flightmodel/engine/ENGN_EGT",    -200   )
 
 set( "sim/cockpit/radios/transponder_mode",   0 )
 
function draw_trim_info2()
  
  if (num == "N394TY" or num == "N394TN") then
    set( "sim/cockpit/radios/transponder_mode",   2 )
  end
  if (num == "E-2C ") then
    set( "sim/cockpit/radios/transponder_mode",   3 )
  end
  if (num == "T45") then
    set( "sim/cockpit/radios/transponder_mode",   4 )
  end
  --/--------------------------------
  
  --FA-18 receives nose authority when landing
  if (0.91 < gears and num == "N394TY")  then
  set ("sim/flightmodel/forces/M_plug_acf" , (50000) )
 end
 --scale to integer
 reset2=bb*100
 --need to set value. This is the reset function
 if (reset2 > 400) then
 set(  "sim/flightmodel/position/local_x",    xboat   )
 set(  "sim/flightmodel/position/local_y",    110    )
 set(  "sim/flightmodel/position/local_z" , (2030 + (zboat)) )
 set(  "sim/flightmodel/position/groundspeed",    -99   )
 set(  "sim/flightmodel/position/local_vz",    -99   )
 
  set(  "sim/operation/override/override_joystick_heading",  1  )
 set(  "sim/flightmodel/position/hpath",  60 )
 set(  "sim/operation/override/override_joystick_pitch",  0  )
 end
 
 
end

do_every_draw("draw_trim_info2()")



