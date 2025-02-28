using ProjectMMOConfigurator.Models;

namespace ProjectMMOConfigurator.Forms
{
    public partial class SkillEditForm : Form
    {
        private readonly Skill _skill;
        private readonly bool _isNewSkill;
        private readonly string _skillName;
        private readonly Action<string, Skill> _saveCallback;

        public SkillEditForm(string skillName, Skill skill, Action<string, Skill> saveCallback, bool isNewSkill = false)
        {
            InitializeComponent();
            NewInitializeComponent();
            _skill = skill;
            _skillName = skillName;
            _saveCallback = saveCallback;
            _isNewSkill = isNewSkill;

            Load += SkillEditForm_Load;
        }

        private void NewInitializeComponent()
        {
            this.txtSkillName = new TextBox();
            this.lblSkillName = new Label();
            this.lblIcon = new Label();
            this.txtIcon = new TextBox();
            this.lblMaxLevel = new Label();
            this.numMaxLevel = new NumericUpDown();
            this.lblIconSize = new Label();
            this.numIconSize = new NumericUpDown();
            this.chkUseTotalLevels = new CheckBox();
            this.lblColor = new Label();
            this.numColor = new NumericUpDown();
            this.chkNoAfkPenalty = new CheckBox();
            this.chkDisplayGroupName = new CheckBox();
            this.chkShowInList = new CheckBox();
            this.lblGroupFor = new Label();
            this.btnAddGroupFor = new Button();
            this.dataGridGroupFor = new DataGridView();
            this.colGroupName = new DataGridViewTextBoxColumn();
            this.colMultiplier = new DataGridViewTextBoxColumn();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            ((System.ComponentModel.ISupportInitialize)this.numMaxLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.numIconSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.numColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.dataGridGroupFor).BeginInit();
            this.SuspendLayout();

            // Form settings
            this.ClientSize = new Size(500, 600);
            this.Text = _isNewSkill ? "Add New Skill" : "Edit Skill";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Skill Name
            this.lblSkillName.AutoSize = true;
            this.lblSkillName.Location = new Point(20, 20);
            this.lblSkillName.Text = "Skill Name:";

            this.txtSkillName.Location = new Point(150, 20);
            this.txtSkillName.Size = new Size(300, 23);

            // Icon
            this.lblIcon.AutoSize = true;
            this.lblIcon.Location = new Point(20, 50);
            this.lblIcon.Text = "Icon Path:";

            this.txtIcon.Location = new Point(150, 50);
            this.txtIcon.Size = new Size(300, 23);

            // Max Level
            this.lblMaxLevel.AutoSize = true;
            this.lblMaxLevel.Location = new Point(20, 80);
            this.lblMaxLevel.Text = "Max Level:";

            this.numMaxLevel.Location = new Point(150, 80);
            this.numMaxLevel.Size = new Size(150, 23);
            this.numMaxLevel.Maximum = int.MaxValue;

            // Icon Size
            this.lblIconSize.AutoSize = true;
            this.lblIconSize.Location = new Point(20, 110);
            this.lblIconSize.Text = "Icon Size:";

            this.numIconSize.Location = new Point(150, 110);
            this.numIconSize.Size = new Size(150, 23);
            this.numIconSize.Maximum = 128;
            this.numIconSize.Minimum = 1;

            // Use Total Levels
            this.chkUseTotalLevels.AutoSize = true;
            this.chkUseTotalLevels.Location = new Point(20, 140);
            this.chkUseTotalLevels.Text = "Use Total Levels";
            this.chkUseTotalLevels.Size = new Size(150, 24);

            // Color
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new Point(20, 170);
            this.lblColor.Text = "Color:";

            this.numColor.Location = new Point(150, 170);
            this.numColor.Size = new Size(150, 23);
            this.numColor.Maximum = int.MaxValue;

            // No Afk Penalty
            this.chkNoAfkPenalty.AutoSize = true;
            this.chkNoAfkPenalty.Location = new Point(20, 200);
            this.chkNoAfkPenalty.Text = "No AFK Penalty";
            this.chkNoAfkPenalty.Size = new Size(150, 24);

            // Display Group Name
            this.chkDisplayGroupName.AutoSize = true;
            this.chkDisplayGroupName.Location = new Point(200, 200);
            this.chkDisplayGroupName.Text = "Display Group Name";
            this.chkDisplayGroupName.Size = new Size(150, 24);

            // Show In List
            this.chkShowInList.AutoSize = true;
            this.chkShowInList.Location = new Point(20, 230);
            this.chkShowInList.Text = "Show In List";
            this.chkShowInList.Size = new Size(150, 24);

            // Group For
            this.lblGroupFor.AutoSize = true;
            this.lblGroupFor.Location = new Point(20, 270);
            this.lblGroupFor.Text = "Group For:";

            this.btnAddGroupFor.Location = new Point(380, 270);
            this.btnAddGroupFor.Size = new Size(70, 25);
            this.btnAddGroupFor.Text = "Add";
            this.btnAddGroupFor.Click += BtnAddGroupFor_Click;

            this.dataGridGroupFor.Location = new Point(20, 300);
            this.dataGridGroupFor.Size = new Size(430, 200);
            this.dataGridGroupFor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridGroupFor.RowHeadersVisible = false;
            this.dataGridGroupFor.AllowUserToAddRows = false;
            this.dataGridGroupFor.Columns.AddRange(new DataGridViewColumn[] {
                this.colGroupName,
                this.colMultiplier
            });

            this.colGroupName.HeaderText = "Group Name";
            this.colGroupName.Name = "colGroupName";

            this.colMultiplier.HeaderText = "Multiplier";
            this.colMultiplier.Name = "colMultiplier";

            // Buttons
            this.btnSave.Location = new Point(270, 520);
            this.btnSave.Size = new Size(100, 30);
            this.btnSave.Text = "Save";
            this.btnSave.Click += BtnSave_Click;

            this.btnCancel.Location = new Point(380, 520);
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += BtnCancel_Click;

            // Add controls
            this.Controls.AddRange(new Control[] {
                this.lblSkillName,
                this.txtSkillName,
                this.lblIcon,
                this.txtIcon,
                this.lblMaxLevel,
                this.numMaxLevel,
                this.lblIconSize,
                this.numIconSize,
                this.chkUseTotalLevels,
                this.lblColor,
                this.numColor,
                this.chkNoAfkPenalty,
                this.chkDisplayGroupName,
                this.chkShowInList,
                this.lblGroupFor,
                this.btnAddGroupFor,
                this.dataGridGroupFor,
                this.btnSave,
                this.btnCancel
            });

            ((System.ComponentModel.ISupportInitialize)this.numMaxLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.numIconSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.numColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.dataGridGroupFor).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void SkillEditForm_Load(object sender, EventArgs e)
        {
            // Populate the form with existing skill data
            txtSkillName.Text = _skillName;
            txtSkillName.Enabled = _isNewSkill; // Only allow editing name for new skills

            txtIcon.Text = _skill.icon;
            numMaxLevel.Value = _skill.maxLevel;
            numIconSize.Value = _skill.iconSize;
            chkUseTotalLevels.Checked = _skill.useTotalLevels;
            numColor.Value = _skill.color;
            chkNoAfkPenalty.Checked = _skill.noAfkPenalty;
            chkDisplayGroupName.Checked = _skill.displayGroupName;
            chkShowInList.Checked = _skill.showInList;

            // Populate groupfor data
            if (_skill.groupfor != null)
            {
                foreach (var kvp in _skill.groupfor)
                {
                    _ = dataGridGroupFor.Rows.Add(kvp.Key, kvp.Value);
                }
            }
        }

        private void BtnAddGroupFor_Click(object sender, EventArgs e) =>
            // Add an empty row
            dataGridGroupFor.Rows.Add(string.Empty, 1.0f);

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtSkillName.Text))
            {
                _ = MessageBox.Show("Skill name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtIcon.Text))
            {
                _ = MessageBox.Show("Icon path cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update skill object
            _skill.icon = txtIcon.Text;
            _skill.maxLevel = (int)numMaxLevel.Value;
            _skill.iconSize = (int)numIconSize.Value;
            _skill.useTotalLevels = chkUseTotalLevels.Checked;
            _skill.color = (int)numColor.Value;
            _skill.noAfkPenalty = chkNoAfkPenalty.Checked;
            _skill.displayGroupName = chkDisplayGroupName.Checked;
            _skill.showInList = chkShowInList.Checked;

            // Update groupfor dictionary
            _skill.groupfor.Clear();
            foreach (DataGridViewRow row in dataGridGroupFor.Rows)
            {
                if (row.Cells[0].Value != null && !string.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                {
                    var groupName = row.Cells[0].Value.ToString();
                    var multiplier = 1.0f;

                    if (row.Cells[1].Value != null)
                    {
                        if (float.TryParse(row.Cells[1].Value.ToString(), out var value))
                        {
                            multiplier = value;
                        }
                    }

                    _skill.groupfor[groupName] = multiplier;
                }
            }

            // Save the skill
            _saveCallback(txtSkillName.Text, _skill);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Control declarations
        private TextBox txtSkillName;
        private Label lblSkillName;
        private Label lblIcon;
        private TextBox txtIcon;
        private Label lblMaxLevel;
        private NumericUpDown numMaxLevel;
        private Label lblIconSize;
        private NumericUpDown numIconSize;
        private CheckBox chkUseTotalLevels;
        private Label lblColor;
        private NumericUpDown numColor;
        private CheckBox chkNoAfkPenalty;
        private CheckBox chkDisplayGroupName;
        private CheckBox chkShowInList;
        private Label lblGroupFor;
        private Button btnAddGroupFor;
        private DataGridView dataGridGroupFor;
        private DataGridViewTextBoxColumn colGroupName;
        private DataGridViewTextBoxColumn colMultiplier;
        private Button btnSave;
        private Button btnCancel;
    }
}