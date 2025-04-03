using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using CDCplusLib.Common.GUI;
using CDCplusLib.DataModel;

namespace CDCplusLib.EventData
{
    public class WindowSelectionData
    {
        public WindowSelectionData()
        {
            RootNodeType = SessionTree.RootNodeTypes.Undefined;
            Selection = new Dictionary<long, IRepositoryNode>();
            Modification = new Dictionary<long, IRepositoryNode>();
            ResultList = new Dictionary<long, IRepositoryNode>();
            SelectedFolder = null;
        }
        public Dictionary<long, IRepositoryNode> Selection { get; set; }
        public Dictionary<long, IRepositoryNode> Modification { get; set; }
        public Dictionary<long, IRepositoryNode> ResultList { get; set; }
        public CmnFolder SelectedFolder { get; set; }
        public SessionTree.RootNodeTypes RootNodeType { get; set; }
    }
}
