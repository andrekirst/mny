use clap::{Parser, Subcommand};

#[derive(Parser)]
#[command(name = "mny")]
#[command(about = "Ein CLI-Tool für Account-Verwaltung", long_about = None)]
struct Cli {
    #[command(subcommand)]
    command: Commands,
}

#[derive(Subcommand)]
enum Commands {
    /// Verwalte Accounts
    Account {
        #[command(subcommand)]
        action: AccountCommands,
    },
}

#[derive(Subcommand)]
enum AccountCommands {
    /// Füge einen neuen Account hinzu
    Add {
        /// Name des Accounts
        #[arg(short, long)]
        name: String,
    },
    /// Wähle einen Account aus
    Select {
        /// ID des Accounts
        id: u32,
    },
    /// Liste alle Accounts
    List,
}

fn main() {
    let cli = Cli::parse();

    match &cli.command {
        Commands::Account { action } => match action {
            AccountCommands::Add { name } => {
                println!("Neuer Account hinzugefügt: {}", name);
                // Füge hier Logik zum Speichern hinzu
            }
            AccountCommands::Select { id } => {
                println!("Account mit ID {} ausgewählt", id);
                // Füge hier Logik zum Auswählen hinzu
            }
            AccountCommands::List => {
                println!("Liste aller Accounts:");
                // Füge hier Logik zum Anzeigen hinzu
            }
        },
    }
}
