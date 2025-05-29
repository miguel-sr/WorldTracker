provider "aws" {
  region = var.aws_region
}

resource "aws_dynamodb_table" "users" {
  name         = "Users"
  billing_mode = "PAY_PER_REQUEST"
  hash_key     = "Id"

  attribute {
    name = "Id"
    type = "S"
  }
}

resource "aws_dynamodb_table" "user_favorites" {
  name         = "UserFavorites"
  billing_mode = "PAY_PER_REQUEST"
  hash_key     = "UserId"
  range_key    = "FavoriteIdRaw"

  attribute {
    name = "UserId"
    type = "S"
  }

  attribute {
    name = "FavoriteIdRaw"
    type = "S"
  }
}

resource "aws_dynamodb_table" "countries" {
  name         = "Countries"
  billing_mode = "PAY_PER_REQUEST"
  hash_key     = "Code"

  attribute {
    name = "Code"
    type = "S"
  }

  attribute {
    name = "Category"
    type = "S"
  }

  attribute {
    name = "NameLower"
    type = "S"
  }

  global_secondary_index {
    name            = "Category-NameLower-index"
    hash_key        = "Category"
    range_key       = "NameLower"
    projection_type = "ALL"
  }
}
