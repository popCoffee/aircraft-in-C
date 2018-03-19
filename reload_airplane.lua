

-- load the XPLM library
ffi = require( "ffi" )
if SYSTEM_ARCHITECTURE == 64 then
    XPLM = ffi.load( "XPLM_64" )
else
    XPLM = ffi.load( "XPLM" )
end
    
-- define the XPLMReloadScenery() C-function to be used from Lua
ffi.cdef( "void XPLMPlaceUserAtAirport(const char *)" )

-- define a global function (macros can only access global functions)
--function let_XPLM_reload_the_scenery()
    XPLMSpeakString("Please wait")
    XPLM.XPLMPlaceUserAtAirport('XCARR')
--end


--do_every_draw("let_XPLM_reload_the_scenery()")
-- create the macro
add_macro( "Reload the Plane", "let_XPLM_reload_the_scenery()" )
