using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C4ObjectApi.Interfaces;
using C4ObjectApi.Repository;
using C4ServerConnector;
using C4ServerConnector.Assets;
using CDCplusLib.EventData;
using CDCplusLib.Interfaces;

namespace CDCplusLib.Common.GUI
{
    public partial class SearchResultsPreview : Form
    {
        private CmnSession _s;
        private bool _objects;
        private HashSet<long> _ids;
        public event IGenericFunction.SessionWindowRequestEventHandler SessionWindowRequest;
        public SearchResultsPreview(CmnSession s)
        {
            InitializeComponent();
            _s = s;
            rldNodes.Init(_s);
        }
        public void SetList(Dictionary<long, C4Object> nl)
        {
            rldNodes.NodeList = nl.ToDictionary(
                kvp => kvp.Key,
                kvp => (IC4Node)kvp.Value
            );
            cmdShowResults.Enabled = rldNodes.NodeList.Count > 0;
            _objects = true;
            _ids = nl.Keys.ToHashSet();
            Text = string.Format(Properties.Resources.lblNumResultsFound, _ids.Count().ToString());
        }
        public void SetList(Dictionary<long, C4Folder> nl)
        {
            rldNodes.NodeList = nl.ToDictionary(
                kvp => kvp.Key,
                kvp => (IC4Node)kvp.Value
            );
            cmdShowResults.Enabled=rldNodes.NodeList.Count > 0;
            _objects = false;
            _ids = nl.Keys.ToHashSet();
        }
        private void cmdShowResults_Click(object sender, EventArgs e)
        {
            WindowSelectionData wsd = new WindowSelectionData();
            wsd.RootNodeType = SessionTree.RootNodeTypes.Results;
            if (_objects)
            {
                Dictionary<long, IC4Node> selection = rldNodes.Selection;
                Dictionary<long, CmnObject> results = null;
                if (selection.Count>0)
                {
                    results = _s.GetObjects(selection.Keys.ToHashSet(), false);
                }
                else
                {
                    results = _s.GetObjects(_ids, false);
                }
                wsd.ResultList = results.ToDictionary(
                    kvp => kvp.Key,
                    kvp => (IRepositoryNode)kvp.Value
                );
                SessionWindowRequest?.Invoke(wsd);
            }
            else
            {
                Dictionary<long, IC4Node> selection = rldNodes.Selection;
                Dictionary<long, CmnFolder> results = null;
                if (selection.Count > 0)
                {
                    results = _s.GetFolders(selection.Keys.ToHashSet());
                }
                else
                {
                    results = _s.GetFolders(_ids);
                }
                wsd.ResultList = results.ToDictionary(
                    kvp => kvp.Key,
                    kvp => (IRepositoryNode)kvp.Value
                );
                SessionWindowRequest?.Invoke(wsd);
            }
            Close();
        }
    }
}
