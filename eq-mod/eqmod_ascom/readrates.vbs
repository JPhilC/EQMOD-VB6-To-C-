'set scope = CreateObject("EQMOD_sim.Telescope")
'set scope = CreateObject("EQMOD_2.Telescope")
set scope = CreateObject("EQMOD.Telescope")
scope.Connected = true
msgbox("RARate=" & scope.RightAscensionRate & " DECRate=" & scope.DeclinationRate )
