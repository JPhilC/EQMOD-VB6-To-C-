set scope1 = CreateObject("EQMOD_SIM.Telescope")
scope1.CommandString(":FORM_RESET#")
'set scope = CreateObject("EQMOD_2.Telescope")
set scope = CreateObject("EQMOD.Telescope")
scope.CommandString(":FORM_RESET#")
