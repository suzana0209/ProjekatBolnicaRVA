using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlijentBolnica.Komande
{
    public class CommandExecutor
    {
        Stack<IUndoKomanda> undoStack = new Stack<IUndoKomanda>();
        Stack<IUndoKomanda> redoStack = new Stack<IUndoKomanda>();
        
        public void DodajIIzvrsi(IUndoKomanda novaKomanda)
        {
            novaKomanda.Izvrsi();
            undoStack.Push(novaKomanda);
            redoStack.Clear();
        }

        public void Undo()
        {
            IUndoKomanda undoCommand = undoStack.Pop();
            undoCommand.Vrati();
            redoStack.Push(undoCommand);
        }

        public bool ValidacijaUndo()
        {
            bool vrati = (undoStack.Count > 0) ? true : false;
            return vrati;
        }

        public void Redo()
        {
            IUndoKomanda redoCommand = redoStack.Pop();
            redoCommand.Izvrsi();
            undoStack.Push(redoCommand);
        }

        public bool ValidacijaRedo()
        {
            bool vrati = (redoStack.Count > 0) ? true : false;
            return vrati;
        }
    }
}
