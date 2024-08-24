﻿using AudioPlayer.API;
using Exiled.API.Features;
using PluginAPI.Events;
using Random = System.Random;

namespace NightMode.Handlers;

public class Server
{

    /// <summary>
    ///     disconnect the dummy
    /// </summary>
    /// <param name="e"></param>
    public static void onServerStopping(RoundEndEvent e)
    {
        AudioController.DisconnectDummy();
        AudioController.StopAudio();
    }

    /// <summary>
    ///     Play something when in lobby
    /// </summary>
    public static void onServerStarting()
    {
        
        AudioController.SpawnDummy(99);
        if (Nightmode.Singleton.Config.playOnLobby)
        {
            AudioController.PlayAudioFromFile(Nightmode.Singleton.Config.lobbySong, true, 70f);
        }
    }

    /// <summary>
    ///     Stop any audio if we played before in lobby
    /// </summary>
    public static void onRoundStart()
    {
        Random random = new Random();
        
        //deprecated since the event itself was dodo
        //Exiled.API.Features.Server.ExecuteCommand("stuckService");

        if (Nightmode.Singleton.Config.playOnLobby)
        {
            AudioController.StopAudio();
        }

        Log.Debug("Roll for event? = " + Nightmode.Singleton.Config.eventRand);
        Log.Debug("Probability for event? = " + Nightmode.Singleton.Config.percentage);
        
        if (Nightmode.Singleton.Config.eventRand && random.NextDouble() <= Nightmode.Singleton.Config.percentage/100)
        {
            
            int events = Nightmode.Singleton.Config.events.Length;
            Log.Debug("array length" + events);
            int rand = random.Next(0, events);

            string command = Nightmode.Singleton.Config.events[rand];
            Log.Debug("command from list: " + command);
            Log.Debug("Index: " + rand);
            Log.Debug("percentage = " + Nightmode.Singleton.Config.percentage/100);
            Exiled.API.Features.Server.ExecuteCommand(command + " on");
        }
    }
}