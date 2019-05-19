﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MonteCarloLib
{
    /// <summary>
    /// Represents a node in the game tree
    /// </summary>

    //stole from ryan
    public interface IGameStatus
    {
        /// <summary>
        /// Score of the given game state.
        /// Set by the user.
        /// </summary>
        int Value { get; }

        /// <summary>
        /// Signifies that the game state is a leaf node of the game tree.
        /// Set by the user.
        /// </summary>
        bool IsTerminal { get; }

        /// <summary>
        /// List of possible moves from this game state.
        /// Generated by the user.
        /// </summary>
        IEnumerable<IGameStatus> Moves { get; set; }

        /// <summary>
        /// The numbers of wins via simulation from this game state.
        /// </summary>
        double Wins { get; set; }

        /// <summary>
        /// The total number of simulations run on this game state.
        /// </summary>
        double Simulations { get; set; }

        /// <summary>
        /// Determines if a simulation started from this game state.
        /// </summary>
        bool Visited { get; set; }
    }
}